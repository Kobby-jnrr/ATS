using ATS.DTOs.TeamDTOs;
using ATS.Entities.TeamEntity;

namespace ATS.Mapping;

public static class TeamMemberMappings
{
    public static TeamMember ToEntity(this CreateTeamMemberDTO createTeamMember)
    {
        return new TeamMember
        {
            Name = createTeamMember.Name,
            Email = createTeamMember.Email,
            Role = createTeamMember.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static TeamMember ToEntity(this UpdateTeamMemberDTO updateTeamMember)
    {
        return new TeamMember
        {
            Name = updateTeamMember.Name,
            Email = updateTeamMember.Email,
            Role = updateTeamMember.Role,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static TeamMemberDetailsDTO ToTeamMemberDetailsDTO(this TeamMember teamMember)
    {
        return new TeamMemberDetailsDTO
            (
                teamMember.Id, 
                teamMember.Name, 
                teamMember.Email, 
                teamMember.Role
            );
    }
}