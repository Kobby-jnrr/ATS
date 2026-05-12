using ATS.Entities.ApplicationEntity;

namespace ATS.DTOs.ApplicationDTOs;

public record CreateApplicationDTO
(
    int JobId,
    string CandidateName,
    string CandidateEmail,
    ApplicationStage Stage = ApplicationStage.Applied,
    int? CultureFitScore = null,
    string? CultureFitComment = null,
    int? InterviewScore = null,
    string? InterviewComment = null,
    int? AssessmentScore = null,
    string? AssesmentComment = null
);