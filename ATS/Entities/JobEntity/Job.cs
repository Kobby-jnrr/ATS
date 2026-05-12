using ApplicationTrackingSystem.Entities.ApplicationEntity;
using ApplicationTrackingSystem.Entities.Base;
using ATS.Entities.Base;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationTrackingSystem.Entities.JobEntity
{
    public class Job : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public JobStatus Status { get; set; }
        public ICollection<Application> Applications { get; set; } = new List<Application> { };

    }
}
