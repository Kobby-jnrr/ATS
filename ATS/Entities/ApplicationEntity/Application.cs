using System.ComponentModel.DataAnnotations;
using ATS.Entities.Base;

namespace ATS.Entities.ApplicationEntity
{
    public class Application : BaseEntity
    {
        [Required]
        public int JobId { get; set; }

        [Required]
        public string CandidateName { get; set; } = string.Empty;

        [Required]
        public string CandidateEmail { get; set; } = string.Empty;

        public ApplicationStage Stage { get; set; } = ApplicationStage.Applied;

        [Range(1, 5)]
        public int? CultureFitScore { get; set; }
        public string? CultureFitComment { get; set; }

        [Range(1, 5)]
        public int? InterviewScore { get; set; }
        public string? InterviewComment { get; set; }

        [Range(1, 5)]
        public int? AssessmentScore { get; set; }
        public string? AssesmentComment { get; set; }

        public ICollection<ApplicationNote> Notes { get; set; } = new List<ApplicationNote>();
        public ICollection<StageHistory> StageHistories { get; set; } = new List<StageHistory>();
    }
}