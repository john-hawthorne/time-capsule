using TimeCapsule.Entities.TaskTypeDtos;

namespace TimeCapsule.Services
{
    public interface ITaskTypeService
    {
        public void AddTaskType(string taskTypeName);
        public IEnumerable<TaskTypeDto> GetTaskTypes();
    }
}
