using ATS.Entities.ApplicationEntity;

namespace ATS.DTOs.ApplicationDTOs;

public record ApplicationDetailsDTO
(
    int Id,

    int JobId,

    string CandidateName,

    string CandidateEmail,

    ApplicationStage Stage,

    int? CultureFitScore,

    string? CultureFitComment,

    int? InterviewScore,

    string? InterviewComment,

    int? AssessmentScore,

    string? AssesmentComment
);