using Microsoft.AspNetCore.Mvc;
using TaskListApp.API.DTOs;
using TaskListApp.API.Mapping;
using TaskListApp.Domain.Interfaces.Services;
using TaskListApp.Domain.Services;

namespace TaskListApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TasksController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTask(CreateTaskItemDto createDto)
        {
            var task = await _taskItemService.CreateTaskAsync(createDto.Title, createDto.Description);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemDto>> UpdateTask(int id, UpdateTaskDto updateDto)
        {
            var task = await _taskItemService.UpdateTaskAsync(id, updateDto.Title, updateDto.Description);
            if (task == null)
                return NotFound();

            return Ok(task.ToDto());
        }

        [HttpPut("{id}/complete")]
        public async Task<ActionResult<TaskItemDto>> CompleteTask(int id)
        {
            var task = await _taskItemService.CompleteTaskAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task.ToDto());
        }

        [HttpPut("{id}/reopen")]
        public async Task<ActionResult<TaskItemDto>> ReopenTask(int id)
        {
            var task = await _taskItemService.ReopenTaskAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskItemService.DeleteTaskAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTask(int id)
        {
            var task = await _taskItemService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task.ToDto());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllTasks()
        {
            var tasks = await _taskItemService.GetAllTasksAsync();
            return Ok(tasks.ToDtos());
        }
    }
}