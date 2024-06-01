using TimeCapsule.Entities.TaskSchedulerDtos;

namespace TimeCapsule.Web.Models;

public class TaskSchedulerModel
{
    public string ScheduleId { get; set; }
    public DateTime ScheduleDate { get; set; }
    public List<TimeSlotModel> TimeSlots { get; set; }

    public TaskSchedulerModel() { }

    public TaskSchedulerModel(TaskSchedulerDto dto)
    {
        ScheduleId = dto.ScheduleId;
        ScheduleDate = dto.ScheduleDate;
        TimeSlots = dto.TimeSlots != null ? dto.TimeSlots.Select(ts => new TimeSlotModel(ts)).ToList() : new List<TimeSlotModel>();
    }
}