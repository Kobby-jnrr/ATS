using ATS.Entities.ApplicationEntity;
using System.ComponentModel.DataAnnotations;

namespace ATS.DTOs.ApplicationDTOs;

public record CreateApplicationDTO
(
    [Required] int JobId,

    [Required] string CandidateName,

    [Required, EmailAddress] string CandidateEmail,

    ApplicationStage Stage = ApplicationStage.Applied,

    [Range(1, 5)] int? CultureFitScore = null,

    string? CultureFitComment = null,

    [Range(1, 5)] int? InterviewScore = null,

    string? InterviewComment = null,

    [Range(1, 5)] int? AssessmentScore = null,

    string? AssesmentComment = null
);
