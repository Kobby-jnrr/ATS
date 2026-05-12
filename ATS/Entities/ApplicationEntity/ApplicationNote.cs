using ATS.Entities.Base;

namespace ATS.Entities.ApplicationEntity
{
    public class ApplicationNote : BaseEntity
    {
        public ApplicationNoteType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CreatedByTeamMemberId { get; set; }
    }
}
