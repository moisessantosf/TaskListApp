import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';
import { TaskFormComponent } from '../task-form/task-form.component';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, TaskFormComponent],
  template: `
    <div class="container mx-auto p-4">
      <h1 class="text-2xl font-bold mb-4">Tasks</h1>
      
      <app-task-form (taskCreated)="refreshTasks()"></app-task-form>

      <div class="mt-4">
        <div *ngFor="let task of tasks" class="bg-white p-4 rounded-lg shadow mb-2">
          <div class="flex justify-between items-center">
            <div>
              <h3 class="text-lg font-semibold" [class.line-through]="task.isCompleted">
                {{ task.title }}
              </h3>
              <p class="text-gray-600">{{ task.description }}</p>
              <p class="text-sm text-gray-500">
                Created: {{ task.createdAt | date }}
              </p>
            </div>
            <div class="space-x-2">
              <button 
                *ngIf="!task.isCompleted"
                (click)="completeTask(task.id)"
                class="bg-green-500 text-white px-3 py-1 rounded">
                Complete
              </button>
              <button 
                *ngIf="task.isCompleted"
                (click)="reopenTask(task.id)"
                class="bg-yellow-500 text-white px-3 py-1 rounded">
                Reopen
              </button>
              <button 
                (click)="deleteTask(task.id)"
                class="bg-red-500 text-white px-3 py-1 rounded">
                Delete
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getAllTasks().subscribe(tasks => {
      this.tasks = tasks;
    });
  }

  refreshTasks(): void {
    this.loadTasks();
  }

  completeTask(id: number): void {
    this.taskService.completeTask(id).subscribe(() => {
      this.loadTasks();
    });
  }

  reopenTask(id: number): void {
    this.taskService.reopenTask(id).subscribe(() => {
      this.loadTasks();
    });
  }

  deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe(() => {
      this.loadTasks();
    });
  }
}