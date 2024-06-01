using TimeCapsule.Entities.TaskTypeDtos;

namespace TimeCapsule.Services
{
    public interface ITaskTypeService
    {
        public IEnumerable<TaskTypeDto> GetTaskTypes();
    }
}
