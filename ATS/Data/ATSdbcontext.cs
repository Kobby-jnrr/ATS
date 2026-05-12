using ATS.Entities.ApplicationEntity;
using ATS.Entities.JobEntity;
using ATS.Entities.TeamEntity;
using Microsoft.EntityFrameworkCore;

namespace ATS.Data;

public class ATSdbcontext(DbContextOptions<ATSdbcontext> options) : DbContext(options)
{
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<ApplicationNote> ApplicationNotes => Set<ApplicationNote>();
    public DbSet<StageHistory> StageHistories => Set<StageHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamMember>().Property(t => t.Role).HasConversion<string>();
        modelBuilder.Entity<Job>().Property(j => j.Status).HasConversion<string>();
        modelBuilder.Entity<Application>().Property(a => a.Stage).HasConversion<string>();
        modelBuilder.Entity<ApplicationNote>().Property(n => n.Type).HasConversion<string>();
        modelBuilder.Entity<StageHistory>().Property(h => h.FromStage).HasConversion<string>();
        modelBuilder.Entity<StageHistory>().Property(h => h.ToStage).HasConversion<string>();

        var seedDate = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<TeamMember>().HasData(
            new
            {
                Id = 1,
                Name = "Kobby Junior",
                Email = "kobbyjunior@gmail.com",
                Role = TeamMemberRole.Recruiter,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new
            {
                Id = 2,
                Name = "Samuel Aikins",
                Email = "samaikins@gmail.com",
                Role = TeamMemberRole.Interviewer,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
              new
              {
                  Id = 3,
                  Name = "Michael Collins",
                  Email = "michaelcollins@gmail.com",
                  Role = TeamMemberRole.HiringTeam,
                  CreatedAt = seedDate,
                  UpdatedAt = seedDate
              }
        );
    }
};
