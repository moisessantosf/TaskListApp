﻿namespace TaskListApp.API.DTOs
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
