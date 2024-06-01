using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeCapsule.Entities.Models;

namespace TimeCapsule.Entities.Context;

public partial class TimeCapsuleDbContext : DbContext
{
    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<Models.Task> Tasks { get; set; }

    public DbSet<TaskType> TaskTypes { get; set; }

    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("TimeCapsuleDev"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
