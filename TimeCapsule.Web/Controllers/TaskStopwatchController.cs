using Microsoft.AspNetCore.Mvc;
using TimeCapsule.Entities.TaskStopwatchDtos;
using TimeCapsule.Services;

namespace TimeCapsule.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskStopwatchController : ControllerBase
{
    private readonly ITaskStopwatchService _taskStopwatchService;
    
    public TaskStopwatchController(ITaskStopwatchService taskStopwatchService)
    {
        _taskStopwatchService = taskStopwatchService;
    }
    
    [HttpGet]
    public IActionResult Get(DateTime selectedDate, int taskTypeId)
    {
        var tasks = _taskStopwatchService.GetTasks(selectedDate, taskTypeId);
        return Ok(tasks);
    }

    [HttpPost]
    public IActionResult Post(string name, int elapsedHour, int elapsedMinute, int elapsedSecond, DateTime startTime,
        int taskTypeId)
    {
        _taskStopwatchService.AddTask(name, elapsedHour, elapsedMinute, elapsedSecond, startTime, taskTypeId);

        return Ok();
    }
    
    [HttpPut]
    public IActionResult Put(string id, string name, DateTime startDate, int startHours, int startMinutes, int startSeconds, string startPeriod, DateTime endDate,
        int endHours, int endMinutes, int endSeconds, string endPeriod, int taskTypeId)
    {
        _taskStopwatchService.UpdateTask(id, name, startDate, startHours, startMinutes, startSeconds, startPeriod, 
            endDate, endHours, endMinutes, endSeconds, endPeriod, taskTypeId);

        return Ok();
    }

    [HttpDelete] 
    public IActionResult Delete(string id) 
    {
        _taskStopwatchService.DeleteTask(id);

        return Ok();
    }
}