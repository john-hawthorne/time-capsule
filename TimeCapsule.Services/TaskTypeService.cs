using Microsoft.EntityFrameworkCore;
using TimeCapsule.Entities.Context;
using TimeCapsule.Entities.Models;
using TimeCapsule.Entities.TaskTypeDtos;

namespace TimeCapsule.Services
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly DbContext _context;
        private readonly DbSet<TaskType> _taskTypes;

        public TaskTypeService(TimeCapsuleDbContext timeCapsuleDbContext)
        {
            _context = timeCapsuleDbContext;
            _taskTypes = _context.Set<TaskType>();
        }

        public void AddTaskType(string taskTypeName)
        {
            var taskType = new TaskType();
            taskType.Name = taskTypeName;
            _taskTypes.Add(taskType);

            _context.SaveChanges();
        }

        public IEnumerable<TaskTypeDto> GetTaskTypes()
        {
            var taskTypes = _taskTypes;
            return taskTypes.Select(tt => new TaskTypeDto(tt)).ToList();
        }
    }
}
