using TaskListApp.API.DTOs;
using TaskListApp.Domain.Entities;

namespace TaskListApp.API.Mapping
{
    public static class TaskItemMapper
    {
        public static TaskItemDto ToDto(this TaskItem task)
        {
            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                CompletedAt = task.CompletedAt
            };
        }

        public static IEnumerable<TaskItemDto> ToDtos(this IEnumerable<TaskItem> tasks)
        {
            return tasks.Select(t => t.ToDto());
        }
    }
}
