using Task = TimeCapsule.Entities.Models.Task;

namespace TimeCapsule.Entities.TaskStopwatchDtos;

public class TaskStopwatchDto
{
    public string Id { get; set; }
    public int TaskTypeId { get; set; }
    public string Name { get; set; }
    public string ElapsedTime { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public TaskStopwatchDto(Task task)
    {
        Id = task.Id.ToString().Substring(0, 7);
        TaskTypeId = task.TaskTypeId;
        Name = task.Name;
        ElapsedTime = task.ElapsedTime;
        StartTime = task.StartTime.ToString();
        EndTime = task.EndTime.ToString();
    }
}