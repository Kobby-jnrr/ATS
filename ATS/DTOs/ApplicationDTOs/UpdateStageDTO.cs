using ATS.Entities.ApplicationEntity;
using System.ComponentModel.DataAnnotations;

namespace ATS.DTOs.ApplicationDTOs;

public record UpdateStageDTO(
    [Required] ApplicationStage NewStage,
    string? Comment
);