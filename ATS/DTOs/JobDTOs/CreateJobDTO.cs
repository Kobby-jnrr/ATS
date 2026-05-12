using ATS.Entities.JobEntity;

namespace ATS.DTOs.JobDTOs;

public record CreateJobDTO
(
    string Title,
    string Description,
    string Location,
    JobStatus Status = JobStatus.Open
);
