using ATS.Entities.Base;

namespace ATS.Entities.TeamEntity;

public class TeamMember : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TeamMemberRole Role { get; set; }
}
