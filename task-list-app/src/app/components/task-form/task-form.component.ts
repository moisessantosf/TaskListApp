import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { CreateTask } from '../../models/task.model';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <form (ngSubmit)="onSubmit()" class="bg-white p-4 rounded-lg shadow mb-4">
      <div class="mb-4">
        <label class="block text-gray-700 text-sm font-bold mb-2" for="title">
          Title
        </label>
        <input
          type="text"
          id="title"
          [(ngModel)]="task.title"
          name="title"
          class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          required
        >
      </div>
      
      <div class="mb-4">
        <label class="block text-gray-700 text-sm font-bold mb-2" for="description">
          Description
        </label>
        <textarea
          id="description"
          [(ngModel)]="task.description"
          name="description"
          class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          rows="3"
        ></textarea>
      </div>

      <button 
        type="submit"
        class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
        Add Task
      </button>
    </form>
  `
})
export class TaskFormComponent {
  @Output() taskCreated = new EventEmitter<void>();

  task: CreateTask = {
    title: '',
    description: ''
  };

  constructor(private taskService: TaskService) {}

  onSubmit(): void {
    if (this.task.title.trim()) {
      this.taskService.createTask(this.task).subscribe(() => {
        this.taskCreated.emit();
        this.task = {
          title: '',
          description: ''
        };
      });
    }
  }
}