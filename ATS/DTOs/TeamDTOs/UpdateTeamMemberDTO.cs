using ATS.Entities.TeamEntity;

namespace ATS.DTOs.TeamDTOs;

public record UpdateTeamMemberDTO
    (
    int Id,
    string Name,
    string Email,
    TeamMemberRole Role
);