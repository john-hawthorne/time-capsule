using Microsoft.EntityFrameworkCore;
using TimeCapsule.Entities.Context;
using TimeCapsule.Entities.Models;
using TimeCapsule.Entities.TaskStopwatchDtos;
using Task = TimeCapsule.Entities.Models.Task;

namespace TimeCapsule.Services;

public class TaskStopwatchService : ITaskStopwatchService
{
    private readonly DbContext _context;
    private readonly DbSet<Task> _tasks;
    private readonly DbSet<TaskType> _taskTypes;

    public TaskStopwatchService(TimeCapsuleDbContext timeCapsuleDbContext)
    {
        _context = timeCapsuleDbContext;
        _tasks = _context.Set<Task>();
        _taskTypes = _context.Set<TaskType>();
    }

    public IEnumerable<TaskStopwatchDto> GetTasks(DateTime selectedDate, int taskTypeId)
    {
        IEnumerable<Task> tasks;
        // 1) Query database for tasks
        if (taskTypeId != 1)
        {
            tasks = _tasks.Where(t => t.TaskType.Id == taskTypeId && t.TimeSlotId == null &&
                (t.StartTime.Month == selectedDate.Month && t.StartTime.Day == selectedDate.Day && t.StartTime.Year == selectedDate.Year)).OrderBy(t => t.StartTime);
        }
        else
        {
            tasks = _tasks.Where(t => t.TimeSlotId == null &&
                (t.StartTime.Month == selectedDate.Month && t.StartTime.Day == selectedDate.Day && t.StartTime.Year == selectedDate.Year)).OrderBy(t => t.StartTime);
        }

        // 2) Convert tasks to dtos & 3) Return dtos
        return tasks.Select(task => new TaskStopwatchDto(task)).ToList();
    }

    public void AddTask(string name, int StopwatchHour, int StopwatchMinute, int StopwatchSecond, DateTime startTime, int taskTypeId)
    {
        var ts = new TimeSpan(StopwatchHour, StopwatchMinute, StopwatchSecond);
        var endTime = startTime.Add(ts);
        var task = new Task();
        task.Id = Guid.NewGuid();
        task.TaskType = _taskTypes.Single(tt => tt.Id == taskTypeId);
        task.Name = name;
        task.StartTime = startTime;
        task.EndTime = endTime;
        task.ElapsedTime = ts.ToString("c");
//        task.UserId = Guid.Parse("8be245ce-60f6-4ef6-9eb2-05c5fd3df22d");
        _tasks.Add(task);

        _context.SaveChanges();
    }
    public void addManualTask(string id, string name, DateTime startDate, int startHours, int startMinutes, int startSeconds, string startPeriod, DateTime endDate, int endHours, int endMinutes, int endSeconds, string endPeriod, int taskTypeId)
    {
        var startTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, DetermineHours(startHours, startPeriod), startMinutes, startSeconds);
        var endTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, DetermineHours(endHours, endPeriod), endMinutes, endSeconds);
        var elapsedTime = endTime.Subtract(startTime);
        var task = new Task();
        task.Id = Guid.NewGuid();
        task.Name = name;
        task.TaskType = _taskTypes.Single(tt => tt.Id == taskTypeId);
        task.StartTime = startTime;
        task.EndTime = endTime;
        var hours = CombineDaysAndHours(elapsedTime.Days, elapsedTime.Hours);
        var strHours = hours < 10 ? "0" + hours : hours.ToString();
        task.ElapsedTime = strHours + ":" + elapsedTime.ToString("mm\\:ss");
        _tasks.Add(task);
        _context.SaveChanges();
    }

    public void UpdateTask(string id, string name, DateTime startDate, int startHours, int startMinutes, int startSeconds, string startPeriod, 
        DateTime endDate, int endHours, int endMinutes, int endSeconds, string endPeriod, int taskTypeId)
    {
        if (startHours == 0)
        {
            var task = _tasks.Single(t => t.Id.ToString().Contains(id));
            task.Name = name;
            task.TaskType = _taskTypes.Single(tt => tt.Id == taskTypeId);
            _context.SaveChanges();
        }
        else
        {
            var startTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, DetermineHours(startHours, startPeriod), startMinutes, startSeconds);
            var endTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, DetermineHours(endHours, endPeriod), endMinutes, endSeconds);
            var elapsedTime = endTime.Subtract(startTime);
            var task = _tasks.Single(t => t.Id.ToString().Contains(id));
            task.Name = name;
            task.TaskType = _taskTypes.Single(tt => tt.Id == taskTypeId);
            task.StartTime = startTime;
            task.EndTime = endTime;
            var hours = CombineDaysAndHours(elapsedTime.Days, elapsedTime.Hours);
            var strHours = hours < 10 ? "0" + hours : hours.ToString();
            task.ElapsedTime = strHours + ":" + elapsedTime.ToString("mm\\:ss");
            _context.SaveChanges();
        }
    }

    public void DeleteTask(string id)
    {
        var task = _tasks.Single(t => t.Id.ToString().Contains(id));
        _tasks.Remove(task);
        _context.SaveChanges();
    }

    private int DetermineHours(int hours, string period)
    {
        int mHours = 0;
        if (period == "AM")
        {
            if (hours == 12)
            {
                mHours = 0;
            }
            else
            {
                mHours = hours;
            }
        }
        else if (period == "PM")
        {
            if (hours == 12)
            {
                mHours = hours;
            }
            else
            {
                mHours = hours + 12;
            }
        }

        return mHours;
    }

    private int CombineDaysAndHours(int days, int hours)
    {
        return days * 24 + hours;
    }

}
