using TimeCapsule.Entities.Models;
using Task = TimeCapsule.Entities.Models.Task;

namespace TimeCapsule.Entities.TaskSchedulerDtos;

public class TimeSlotDto
{
    public Guid TimeSlotId { get; set; }
    public Guid TaskId { get; set; }
    public DateTime SlotTime { get; set; }
    public string TaskName { get; set; }

    public TimeSlotDto(TimeSlot timeSlot)
    {
        TimeSlotId = timeSlot.Id;
        TaskId = timeSlot.Task.Id;
        SlotTime = timeSlot.SlotTime;
        TaskName = timeSlot.Task.Name;
    }
}