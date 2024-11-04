using System.ComponentModel.DataAnnotations;

namespace TaskListApp.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        public TaskItem(string title, string? description = null)
        {
            Title = title;
            Description = description;
            IsCompleted = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string title, string? description)
        {
            Title = title;
            Description = description;
        }

        public void Complete()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                CompletedAt = DateTime.UtcNow;
            }
        }

        public void Reopen()
        {
            if (IsCompleted)
            {
                IsCompleted = false;
                CompletedAt = null;
            }
        }
    }
}