using System.ComponentModel.DataAnnotations;

namespace TimeCapsule.Entities.Models;

public class Schedule
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public DateTime ScheduleDate { get; set; }
    public ICollection<TimeSlot> TimeSlots { get; } = new List<TimeSlot>();
}
