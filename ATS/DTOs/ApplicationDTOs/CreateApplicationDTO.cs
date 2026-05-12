using System.ComponentModel.DataAnnotations;

namespace ATS.DTOs.ApplicationDTOs;

public record CreateApplicationDTO(
    [Required] string CandidateName,

    [Required, EmailAddress] string CandidateEmail
);