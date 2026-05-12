using ATS.Entities.ApplicationEntity;
using System.ComponentModel.DataAnnotations;

namespace ATS.DTOs.ApplicationDTOs;

public record UpdateApplicationDTO
(
    [Required] int Id,

    [Required] int JobId,

    [Required] string CandidateName,

    [Required, EmailAddress] string CandidateEmail,

    ApplicationStage Stage,

    [Range(1, 5)] int? CultureFitScore = null,

    string? CultureFitComment = null,

    [Range(1, 5)] int? InterviewScore = null,

    string? InterviewComment = null,

    [Range(1, 5)] int? AssessmentScore = null,

    string? AssesmentComment = null
);