using TaskListApp.Domain.Entities;

namespace TaskListApp.Domain.Interfaces.Repositories
{
    public interface ITaskItemRepository
    {
        Task<TaskItem> AddAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
    }
}
