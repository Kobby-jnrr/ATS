using ATS.DTOs.ApplicationDTOs;
using ATS.Entities.JobEntity;

namespace ATS.DTOs.JobDTOs;

public record JobDetailsDTO
(
    int Id,
    string Title,
    string Description,
    string Location,
    JobStatus Status,
    IEnumerable<ApplicationDetailsDTO> Applications
);
