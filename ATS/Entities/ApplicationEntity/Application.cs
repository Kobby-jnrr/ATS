using ATS.Entities.Base;

namespace ATS.Entities.ApplicationEntity
{
    public class Application : BaseEntity
    {
        public int JobId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string CandidateEmail { get; set; } = string.Empty;
        public ApplicationStage Stage { get; set; } = ApplicationStage.Applied;
        
        public int? CultureFitScore { get; set; }
        public string? CultureFitComment { get; set; }
        public int? InterviewScore { get; set; }
        public string? InterviewComment { get; set; }
        public int? AssessmentScore { get; set; }
        public string? AssesmentComment { get; set; }
        public ICollection<ApplicationNote> Notes { get; set; } = new List<ApplicationNote> { };
        public ICollection<StageHistory> StageHistories { get; set; } = new List<StageHistory> { };
    }
}
