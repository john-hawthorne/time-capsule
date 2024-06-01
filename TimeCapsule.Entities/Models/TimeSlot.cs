using System.ComponentModel.DataAnnotations;

namespace TimeCapsule.Entities.Models;

public class TimeSlot
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public DateTime SlotTime { get; set; }

    public Task? Task { get; set; }

    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = null!;
}
