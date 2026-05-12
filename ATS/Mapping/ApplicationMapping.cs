using ATS.DTOs.ApplicationDTOs;
using ATS.Entities.ApplicationEntity;

namespace ATS.Mapping;

public static class ApplicationMapping
{
    public static Application ToEntity(this CreateApplicationDTO dto, int jobId)
    {
        return new Application
        {
            JobId = jobId,
            CandidateName = dto.CandidateName,
            CandidateEmail = dto.CandidateEmail,
            Stage = ApplicationStage.Applied
        };
    }

    public static ApplicationDetailsDTO ToApplicationDetailsDTO(this Application application)
    {
        return new ApplicationDetailsDTO(
            application.Id,
            application.JobId,
            application.CandidateName,
            application.CandidateEmail,
            application.Stage,
            application.CultureFitScore,
            application.CultureFitComment,
            application.InterviewScore,
            application.InterviewComment,
            application.AssessmentScore,
            application.AssesmentComment);
    }
}