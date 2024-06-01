using TimeCapsule.Entities.Models;
using Task = TimeCapsule.Entities.Models.Task;

namespace TimeCapsule.Entities.TaskSchedulerDtos;

public class TaskSchedulerDto
{
    public string ScheduleId { get; set; }
    public DateTime ScheduleDate { get; set; }
    public List<TimeSlotDto> TimeSlots { get; set; }

    public TaskSchedulerDto() { }

    public TaskSchedulerDto(Schedule schedule)
    {
        ScheduleId = schedule.Id.ToString().Substring(0, 7);
        ScheduleDate = schedule.ScheduleDate;
        TimeSlots = schedule.TimeSlots.OrderBy(ts => ts.SlotTime).Select(ts => new TimeSlotDto(ts)).ToList();
    }
}