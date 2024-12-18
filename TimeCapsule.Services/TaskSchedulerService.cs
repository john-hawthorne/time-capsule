using Microsoft.EntityFrameworkCore;
using TimeCapsule.Entities.Context;
using TimeCapsule.Entities.Models;
using TimeCapsule.Entities.TaskSchedulerDtos;
using Task = TimeCapsule.Entities.Models.Task;

namespace TimeCapsule.Services;

public class TaskSchedulerService : ITaskSchedulerService
{
    private readonly DbContext _context;
    private readonly DbSet<Task> _tasks;
    private readonly DbSet<TaskType> _taskTypes;
    private readonly DbSet<TimeSlot> _timeSlots;
    private readonly DbSet<Schedule> _schedules;

    public TaskSchedulerService(TimeCapsuleDbContext timeCapsuleDbContext)
    {
        _context = timeCapsuleDbContext;
        _tasks = _context.Set<Task>();
        _taskTypes = _context.Set<TaskType>();
        _timeSlots = _context.Set<TimeSlot>();
        _schedules = _context.Set<Schedule>();
    }

    public TaskSchedulerDto GetSchedule(DateTime scheduleDate)
    {
        var schedule = _schedules.Include(s => s.TimeSlots).ThenInclude(ts => ts.Task).SingleOrDefault(sched => sched.ScheduleDate == scheduleDate.Date);
        return schedule == null ? new TaskSchedulerDto() : new TaskSchedulerDto(schedule);
    }

    public void AddSchedule(string[] taskNames, DateTime scheduleDate, int taskTypeId)
    {
        // 1) Create Schedule
        var schedule = new Schedule
        {
            Id = Guid.NewGuid(),
            ScheduleDate = scheduleDate.Date,
        };
        
        // 2) Create Tasks From TaskNames
        var tasks = CreateTasks(taskNames, scheduleDate, taskTypeId);

        // 3) Add Tasks to Schedule
        var freshTimeSlots = AssignTasksToTimeSlots(schedule.Id, scheduleDate, tasks);

        // 4) Save Tasks, Timeslots, Schedule to database
        foreach (var task in tasks)
        {
            _tasks.Add(task);
        }
        foreach(var timeSlot in freshTimeSlots)
        {
            _timeSlots.Add(timeSlot);
        }
        _schedules.Add(schedule);
        _context.SaveChanges();
    }

    public void UpdateSchedule(string[] taskNames, DateTime scheduleDate, int taskTypeId)
    {
        // 1) Remove schedule
        DeleteSchedule(scheduleDate);
        
        // 1) Create Schedule
        var schedule = new Schedule
        {
            Id = Guid.NewGuid(),
            ScheduleDate = scheduleDate.Date,
        };
        
        // 2) Create Tasks From TaskNames
        var tasks = CreateTasks(taskNames, scheduleDate, taskTypeId);
        
        // 3) Add Tasks to Schedule
        var freshTimeSlots = AssignTasksToTimeSlots(schedule.Id, scheduleDate, tasks);

        // 4) Save Tasks, Timeslots, and Schedule to database
        foreach (var task in tasks)
        {
            _tasks.Add(task);
        }
        foreach(var timeSlot in freshTimeSlots)
        {
            _timeSlots.Add(timeSlot);
        }
        _schedules.Add(schedule);
        _context.SaveChanges();
    }

    public void DeleteSchedule(DateTime scheduleDate)
    {
        var existingSchedule = _schedules.Include(s => s.TimeSlots).ThenInclude(ts => ts.Task).Single(s => s.ScheduleDate == scheduleDate);

        foreach (var timeSlot in existingSchedule.TimeSlots)
        {
            var task = _tasks.Single(t => t.Id == timeSlot.Task.Id);
            _tasks.Remove(task);
            _timeSlots.Remove(timeSlot);
        }

        _schedules.Remove(existingSchedule);
        _context.SaveChanges();
    }

    private List<Task> CreateTasks(string[] taskNames, DateTime scheduleDate, int taskTypeId)
    {
        // 1) Prepare Task Collection
        var ts = new TimeSpan(0, 0, 0);
        var tasks = new List<Task>();

        // 2) Convert Each Task Name String to a Task Object
        foreach(var taskName in taskNames)
        {
            var task = new Task();
            task.Id = Guid.NewGuid();
            task.Name = taskName != String.Empty ? taskName : "(No name)";
            task.StartTime = scheduleDate.Date;
            task.EndTime = scheduleDate.Date;
            task.ElapsedTime = ts.ToString("c");
            task.TaskType = _taskTypes.Single(tt => tt.Id == taskTypeId);
            tasks.Add(task);
        }

        // 3) Create Placeholder Tasks
        var adjustedCount = 18 - taskNames.Count();
        for(int i = 0; i < adjustedCount; i++)
        {
            var task = new Task();
            task.Id = Guid.NewGuid();
            task.Name = "(No name)";
            task.StartTime = scheduleDate.Date;
            task.EndTime = scheduleDate.Date;
            task.ElapsedTime = ts.ToString("c");
            task.TaskType = _taskTypes.Single(tt => tt.Id == taskTypeId); 
            tasks.Add(task);
        }

        return tasks;
    }

    private List<TimeSlot> AssignTasksToTimeSlots(Guid scheduleId, DateTime scheduleDate, List<Task> tasks)
    {
        // Prepare Timeslot
        var timeSlotsList = new List<TimeSlot>();
        var times = new List<int>() {6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

        // Create Timeslots from Tasks
        foreach (var task in tasks)
        {
            // Prepare General Timeslot Info
            int currentYear = scheduleDate.Date.Year;
            int currentMonth = scheduleDate.Date.Month;
            int currentDay = scheduleDate.Date.Day;
            int startMin = 0;
            int startSec = 0;

            // Create Timeslot
            var dt = new DateTime(currentYear, currentMonth, currentDay, times.First(), startMin, startSec);
            var timeSlot = new TimeSlot();
            timeSlot.Id = Guid.NewGuid();
            timeSlot.ScheduleId = scheduleId;
            timeSlot.SlotTime = dt;
            timeSlot.Task = task;
            timeSlotsList.Add(timeSlot);

            times.Remove(times.First());
        }

        return timeSlotsList;
    }   
}
