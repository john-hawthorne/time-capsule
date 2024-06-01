using TimeCapsule.Entities.Models;

namespace TimeCapsule.Entities.TaskTypeDtos;

public class TaskTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TaskTypeDto(TaskType tt)
    {
        Id = tt.Id;
        Name = tt.Name;
    }
}