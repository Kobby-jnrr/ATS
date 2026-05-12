using ATS.Entities.ApplicationEntity;
namespace ATS.DTOs.ApplicationDTOs;

public record CreateNoteDTO(ApplicationNoteType Type, string Description);