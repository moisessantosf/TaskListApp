using Moq;
using TaskListApp.Domain.Entities;
using TaskListApp.Domain.Interfaces.Repositories;
using TaskListApp.Domain.Services;
using Xunit;

namespace TaskListApp.UnitTests.Services
{
    public class TaskItemServiceTests
    {
        private readonly Mock<ITaskItemRepository> _mockRepository;
        private readonly TaskItemService _service;

        public TaskItemServiceTests()
        {
            _mockRepository = new Mock<ITaskItemRepository>();
            _service = new TaskItemService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateTaskAsync_ShouldCreateAndReturnTask()
        {
            // Arrange
            var title = "Test Task";
            var description = "Test Description";
            var task = new TaskItem(title, description);
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync((TaskItem t) => t);

            // Act
            var result = await _service.CreateTaskAsync(title, description);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
            Assert.Equal(description, result.Description);
            Assert.False(result.IsCompleted);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_WithValidId_ShouldUpdateAndReturnTask()
        {
            // Arrange
            var taskId = 1;
            var existingTask = new TaskItem("Old Title", "Old Description") { Id = taskId };
            var newTitle = "Updated Title";
            var newDescription = "Updated Description";

            _mockRepository.Setup(r => r.GetByIdAsync(taskId))
                .ReturnsAsync(existingTask);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync((TaskItem t) => t);

            // Act
            var result = await _service.UpdateTaskAsync(taskId, newTitle, newDescription);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newTitle, result.Title);
            Assert.Equal(newDescription, result.Description);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task CompleteTaskAsync_WithValidId_ShouldCompleteAndReturnTask()
        {
            // Arrange
            var taskId = 1;
            var existingTask = new TaskItem("Test Task", "Test Description") { Id = taskId };

            _mockRepository.Setup(r => r.GetByIdAsync(taskId))
                .ReturnsAsync(existingTask);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync((TaskItem t) => t);

            // Act
            var result = await _service.CompleteTaskAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsCompleted);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var taskId = 1;
            _mockRepository.Setup(r => r.DeleteAsync(taskId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteTaskAsync(taskId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.DeleteAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<TaskItem>
            {
                new TaskItem("Task 1", "Description 1"),
                new TaskItem("Task 2", "Description 2")
            };
            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(tasks);

            // Act
            var result = await _service.GetAllTasksAsync();

            // Assert
            Assert.Equal(tasks.Count, result.Count());
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}