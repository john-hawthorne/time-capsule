using System.ComponentModel.DataAnnotations;

namespace TimeCapsule.Entities.Models;

public class Schedule
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public DateTime ScheduleDate { get; set; }
    public ICollection<TimeSlot> TimeSlots { get; } = new List<TimeSlot>();

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
