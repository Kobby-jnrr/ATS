using ATS.Entities.TeamEntity;

namespace ATS.DTOs.TeamDTOs;

public record TeamMemberDetailsDTO
(
    int Id,
    string Name,
    string Email,
    TeamMemberRole Role
);