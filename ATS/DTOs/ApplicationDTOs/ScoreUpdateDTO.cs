using System.ComponentModel.DataAnnotations;

namespace ATS.DTOs.ApplicationDTOs;

public record ScoreUpdateDTO(
    [Required, Range(1, 5)] int Score,

    string? Comment
);