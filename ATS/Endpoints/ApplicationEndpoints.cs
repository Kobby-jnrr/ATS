using ATS.Data;
using ATS.DTOs.ApplicationDTOs;
using ATS.Entities.ApplicationEntity;
using Microsoft.EntityFrameworkCore;

namespace ATS.Endpoints;

public static class ApplicationEndpoints
{
    public static void MapApplicationEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/api/jobs/{jobId}/applications", async (int jobId, CreateApplicationDTO dto, ATSdbcontext db) =>
        {
            var job = await db.Jobs.FindAsync(jobId);
            if (job is null) return Results.NotFound("Job not found.");

            var alreadyApplied = await db.Applications
                .AnyAsync(a => a.JobId == jobId && a.CandidateEmail == dto.CandidateEmail);

            if (alreadyApplied) return Results.BadRequest("You have already applied for this position.");

            var application = new Application
            {
                JobId = jobId,
                CandidateName = dto.CandidateName,
                CandidateEmail = dto.CandidateEmail,
                Stage = ApplicationStage.Applied
            };

            db.Applications.Add(application);
            await db.SaveChangesAsync();

            return Results.Created($"/api/applications/{application.Id}", application);
        });

        app.MapGet("/api/jobs/{jobId}/applications", async (int jobId, ATSdbcontext db, ApplicationStage? stage) =>
        {
            var query = db.Applications.Where(a => a.JobId == jobId);
            if (stage.HasValue) query = query.Where(a => a.Stage == stage.Value);

            var results = await query.ToListAsync();
            return Results.Ok(results);
        });

        var group = app.MapGroup("/api/applications");

        group.MapGet("/{id}", async (int id, ATSdbcontext db) =>
        {
            var application = await db.Applications
                .Include(a => a.StageHistories)
                .Include(a => a.Notes)
                .FirstOrDefaultAsync(a => a.Id == id);

            return application is null ? Results.NotFound() : Results.Ok(application);
        });

        group.MapGet("/{id}/audit", async (int id, ATSdbcontext db) =>
        {
            var historyData = await db.StageHistories
                .Where(h => h.ApplicationId == id)
                .Join(db.TeamMembers,
                    h => h.ChangedByTeamMemberId,
                    t => t.Id,
                    (h, t) => new { h, t.Name })
                .OrderByDescending(x => x.h.ChangedAt)
                .ToListAsync();

            var result = historyData.Select(x => new AuditTrailDTO(
                x.h.FromStage.ToString(),
                x.h.ToStage.ToString(),
                x.Name,
                x.h.ChangedAt,
                x.h.Comment
            ));

            return Results.Ok(result);
        });

        group.MapPatch("/{id}/stage", async (int id, UpdateStageDTO dto, ATSdbcontext db, HttpRequest request) =>
        {
            if (!TryGetMemberId(request, out var memberId)) return Results.BadRequest("X-Team-Member-Id required.");

            var app = await db.Applications.FindAsync(id);
            if (app is null) return Results.NotFound();

            if (!IsValidTransition(app.Stage, dto.NewStage))
                return Results.BadRequest($"Invalid transition from {app.Stage} to {dto.NewStage}.");

            db.StageHistories.Add(new StageHistory
            {
                ApplicationId = id,
                FromStage = app.Stage,
                ToStage = dto.NewStage,
                ChangedByTeamMemberId = memberId,
                Comment = dto.Comment
            });

            app.Stage = dto.NewStage;
            app.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { Stage = app.Stage });
        });

        group.MapPut("/{id}/scores/culture-fit", async (int id, ScoreUpdateDTO dto, ATSdbcontext db, HttpRequest request) =>
            await HandleScoreUpdate(id, dto, db, request, "culture"));

        group.MapPut("/{id}/scores/interview", async (int id, ScoreUpdateDTO dto, ATSdbcontext db, HttpRequest request) =>
            await HandleScoreUpdate(id, dto, db, request, "interview"));

        group.MapPut("/{id}/scores/assessment", async (int id, ScoreUpdateDTO dto, ATSdbcontext db, HttpRequest request) =>
            await HandleScoreUpdate(id, dto, db, request, "assessment"));

        group.MapPost("/{id}/notes", async (int id, CreateNoteDTO noteDto, ATSdbcontext db, HttpRequest request) =>
        {
            if (!TryGetMemberId(request, out var memberId)) return Results.BadRequest("X-Team-Member-Id required.");

            var note = new ApplicationNote
            {
                ApplicationId = id,
                Type = noteDto.Type,
                Description = noteDto.Description,
                CreatedByTeamMemberId = memberId
            };

            db.ApplicationNotes.Add(note);
            await db.SaveChangesAsync();
            return Results.Created($"/api/applications/{id}/notes", note);
        });
    }

    private static bool TryGetMemberId(HttpRequest request, out int id)
    {
        id = 0;
        return request.Headers.TryGetValue("X-Team-Member-Id", out var val) && int.TryParse(val, out id);
    }

    private static async Task<IResult> HandleScoreUpdate(int id, ScoreUpdateDTO dto, ATSdbcontext db, HttpRequest request, string type)
    {
        if (!TryGetMemberId(request, out var memberId)) return Results.BadRequest("X-Team-Member-Id required.");

        if (dto.Score < 1 || dto.Score > 5)
            return Results.BadRequest("Score must be between 1 and 5.");

        var app = await db.Applications.FindAsync(id);
        if (app is null) return Results.NotFound();

        if (type == "culture") { app.CultureFitScore = dto.Score; app.CultureFitComment = dto.Comment; }
        else if (type == "interview") { app.InterviewScore = dto.Score; app.InterviewComment = dto.Comment; }
        else if (type == "assessment") { app.AssessmentScore = dto.Score; app.AssesmentComment = dto.Comment; }

        app.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    public static bool IsValidTransition(ApplicationStage from, ApplicationStage to)
    {
        if (to == ApplicationStage.Rejected) return true;
        if (from == ApplicationStage.Rejected || from == ApplicationStage.Hiring) return false;

        return from switch
        {
            ApplicationStage.Applied => to == ApplicationStage.Screening,
            ApplicationStage.Screening => to == ApplicationStage.Interview,
            ApplicationStage.Interview => to == ApplicationStage.Offer,
            ApplicationStage.Offer => to == ApplicationStage.Hiring,
            _ => false
        };
    }
}