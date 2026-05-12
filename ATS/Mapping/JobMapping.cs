using ATS.DTOs.JobDTOs;
using ATS.Entities.JobEntity;

namespace ATS.Mapping;

public static class JobMapping
{
    public static Job ToEntity(this CreateJobDTO createJob)
    {
        return new Job
        {
            Title = createJob.Title,
            Description = createJob.Description,
            Location = createJob.Location,
            Status = createJob.Status
        };
    }

    public static Job ToEntity(this UpdateJobDTO updateJob)
    {
        return new Job
        {
            Id = updateJob.Id,
            Title = updateJob.Title,
            Description = updateJob.Description,
            Location = updateJob.Location,
            Status = updateJob.Status
        };
    }

    public static JobDetailsDTO ToJobDetailsDTO(this Job job)
    {
        return new JobDetailsDTO
            (
                job.Id, 
                job.Title, 
                job.Description, 
                job.Location, 
                job.Status
            );
    }
}
