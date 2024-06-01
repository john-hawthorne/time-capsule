using System.ComponentModel.DataAnnotations;

namespace TimeCapsule.Entities.Models;

public class Task
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public string ElapsedTime { get; set; }

    public Guid? TimeSlotId { get; set; }
    public TimeSlot TimeSlot { get; set; } = null!;

    public TaskType? TaskType { get; set; }
    public int TaskTypeId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
