using ATS.Entities.Base;

namespace ATS.Entities.ApplicationEntity;

public class StageHistory : BaseEntity
{
    public int ApplicationId { get; set; }
    public ApplicationStage FromStage { get; set; }
    public ApplicationStage ToStage { get; set; }
    public int ChangedByTeamMemberId { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public string? Comment { get; set; }
}
