using TimeCapsule.Entities.TaskStopwatchDtos;

namespace TimeCapsule.Services;

public interface ITaskStopwatchService
{
    public IEnumerable<TaskStopwatchDto> GetTasks(DateTime selectedDate, int taskTypeId);

    void AddTask(string name, int timerHour, int timerMinute, int timerSecond, DateTime startTime, int taskTypeId);

    void addManualTask(string id, string name, DateTime startDate, int startHours, int startMinutes, int startSeconds, string startPeriod,
    DateTime endDate, int endHours, int endMinutes, int endSeconds, string endPeriod, int taskTypeId);

    void UpdateTask(string id, string name, DateTime startDate, int startHours, int startMinutes, int startSeconds, string startPeriod, 
        DateTime endDate, int endHours, int endMinutes, int endSeconds, string endPeriod, int taskTypeId);

    void DeleteTask(string id);
}