using ATS.Data;
using ATS.DTOs.JobDTOs;
using ATS.Entities.JobEntity;
using ATS.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ATS.Endpoints;

public static class JobEndpoints
{
    public static void MapJobEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/jobs");

        group.MapGet("/", async (ATSdbcontext db, string? status, int page = 1, int pageSize = 20) =>
        {
            var query = db.Jobs
                .Include(j => j.Applications)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<JobStatus>(status, true, out var jobStatus))
            {
                query = query.Where(j => j.Status == jobStatus);
            }

            var jobs = await query
                .OrderByDescending(j => j.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Results.Ok(jobs.Select(j => j.ToJobDetailsDTO()));
        });

        group.MapGet("/{id}", async (int id, ATSdbcontext db) =>
        {
            var job = await db.Jobs
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(j => j.Id == id);

            return job is not null ? Results.Ok(job) : Results.NotFound();
        });

        group.MapPost("/", async (CreateJobDTO jobDto, ATSdbcontext db, HttpRequest request) =>
        {
            if (!request.Headers.TryGetValue("X-Team-Member-Id", out var _))
                return Results.BadRequest("X-Team-Member-Id header is required.");

            var job = new Job
            {
                Title = jobDto.Title,
                Description = jobDto.Description,
                Location = jobDto.Location,
                Status = JobStatus.Open
            };

            db.Jobs.Add(job);
            await db.SaveChangesAsync();
            return Results.Created($"/api/jobs/{job.Id}", job);
        });
    }
}