using ATS.Entities.TeamEntity;

namespace ATS.DTOs.TeamDTOs;

public record CreateTeamMemberDTO
(
    string Name,
    string Email,
    TeamMemberRole Role
);
