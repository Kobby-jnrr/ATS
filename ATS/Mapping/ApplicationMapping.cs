using ATS.DTOs.ApplicationDTOs;
using ATS.Entities.ApplicationEntity;

namespace ATS.Mapping;

public static class ApplicationMapping
{
    public static Application ToEntity(this CreateApplicationDTO createApplication)
    {
        return new Application
        {
            JobId = createApplication.JobId,

            CandidateName = createApplication.CandidateName,

            CandidateEmail = createApplication.CandidateEmail,

            Stage = createApplication.Stage,

            CultureFitScore = createApplication.CultureFitScore,

            CultureFitComment = createApplication.CultureFitComment,

            InterviewScore = createApplication.InterviewScore,

            InterviewComment = createApplication.InterviewComment,

            AssessmentScore = createApplication.AssessmentScore,

            AssesmentComment = createApplication.AssesmentComment
        };
    }

    public static Application ToEntity(this UpdateApplicationDTO updateApplication)
    {
        return new Application
        {
            Id = updateApplication.Id,

            JobId = updateApplication.JobId,

            CandidateName = updateApplication.CandidateName,

            CandidateEmail = updateApplication.CandidateEmail,

            Stage = updateApplication.Stage,

            CultureFitScore = updateApplication.CultureFitScore,

            CultureFitComment = updateApplication.CultureFitComment,

            InterviewScore = updateApplication.InterviewScore,

            InterviewComment = updateApplication.InterviewComment,

            AssessmentScore = updateApplication.AssessmentScore,

            AssesmentComment = updateApplication.AssesmentComment
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
