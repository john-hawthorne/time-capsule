using TimeCapsule.Entities.TaskSchedulerDtos;

namespace TimeCapsule.Services;

public interface ITaskSchedulerService
{
    public TaskSchedulerDto GetSchedule(DateTime scheduleDate);

    public void AddSchedule(string[] taskNames, DateTime scheduleDate, int taskTypeId);
    public void UpdateSchedule(string[] taskNames, DateTime scheduleDate, int taskTypeId);
}