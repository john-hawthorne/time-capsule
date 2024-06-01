using Microsoft.AspNetCore.Mvc;
using TimeCapsule.Services;
using TimeCapsule.Web.Models;

namespace TimeCapsule.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskSchedulerController : ControllerBase
{
    private readonly ITaskSchedulerService _taskSchedulerService;
    
    public TaskSchedulerController(ITaskSchedulerService taskSchedulerService)
    {
        _taskSchedulerService = taskSchedulerService;
    }

    [HttpGet]
    public IActionResult GetSchedule(DateTime selectedDate)
    {
        var schedule = _taskSchedulerService.GetSchedule(selectedDate);
        var scheduleModel = schedule.ScheduleId == Guid.Empty.ToString().Substring(0, 7) ? new TaskSchedulerModel() : new TaskSchedulerModel(schedule);
        return Ok(scheduleModel);
    }
    
    
    [HttpPost]
    public IActionResult AddSchedule(string[] taskNames, DateTime selectedDate, int taskTypeId)
    {
        _taskSchedulerService.AddSchedule(taskNames, selectedDate, taskTypeId);

        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateSchedule(string[] taskNames, DateTime selectedDate, int taskTypeId)
    {
        _taskSchedulerService.UpdateSchedule(taskNames, selectedDate, taskTypeId);

        return Ok();
    }
}