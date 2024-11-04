using TaskListApp.Domain.Entities;
using TaskListApp.Domain.Interfaces.Repositories;
using TaskListApp.Domain.Interfaces.Services;

namespace TaskListApp.Domain.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskRepository;

        public TaskItemService(ITaskItemRepository taskItemRepository)
        {
            _taskRepository = taskItemRepository;
        }

        public async Task<TaskItem> CreateTaskAsync(string title, string? description)
        {
            var task = new TaskItem(title, description);
            return await _taskRepository.AddAsync(task);
        }

        public async Task<TaskItem?> UpdateTaskAsync(int id, string title, string? description)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return null;

            task.UpdateDetails(title, description);
            return await _taskRepository.UpdateAsync(task);
        }

        public async Task<TaskItem?> CompleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return null;

            task.Complete();
            return await _taskRepository.UpdateAsync(task);
        }

        public async Task<TaskItem?> ReopenTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return null;

            task.Reopen();
            return await _taskRepository.UpdateAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }
    }
}
