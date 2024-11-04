using TaskListApp.Domain.Entities;

namespace TaskListApp.Domain.Interfaces.Services
{
    public interface ITaskItemService
    {
        Task<TaskItem> CreateTaskAsync(string title, string? description);
        Task<TaskItem?> UpdateTaskAsync(int id, string title, string? description);
        Task<TaskItem?> CompleteTaskAsync(int id);
        Task<TaskItem?> ReopenTaskAsync(int id);
        Task<bool> DeleteTaskAsync(int id);
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    }
}
