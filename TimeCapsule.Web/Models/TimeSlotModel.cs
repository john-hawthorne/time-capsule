using TimeCapsule.Entities.TaskSchedulerDtos;

namespace TimeCapsule.Web.Models;

public class TimeSlotModel
{
    public string TimeSlotId { get; set; }
    public string TaskId { get; set; }
    public string TaskName { get; set; }
    public string SlotTime { get; set; }

    public TimeSlotModel(TimeSlotDto dto)
    {
        TimeSlotId = dto.TimeSlotId.ToString().Substring(0, 7);
        TaskId = dto.TaskId.ToString().Substring(0, 7);
        TaskName = dto.TaskName;
        SlotTime = dto.SlotTime.ToString();
    }
}