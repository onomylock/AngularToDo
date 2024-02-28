import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TodoGroupComponent } from "./components/todo-group/todo-group.component";
import { TodoGroup, TodoItem, TodoStatus } from './interfaces/todo-group.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [CommonModule, RouterOutlet, TodoGroupComponent]
})
export class AppComponent {
  title = 'angular-todo';
  public todoGroups: TodoGroup[];

  constructor() {
    this.todoGroups = [{
      title: 'first todo',
      items: [{
        title: 'first item',
        description: 'lalal',
        status: TodoStatus.IN_PROGRESS
      },
      {
        title: 'second item',
        description: 'mememe',
        status: TodoStatus.DONE
      }]
    },
    {
      title: 'second',
      items: [{
        title: 'second item',
        description: 'fdkdfl',
        status: TodoStatus.NOT_STARTED
      }]
    }];
  }

  public addGroup(): void {
    let tmpGroup: TodoGroup = {
      title: '',
      items: []
    };

    this.todoGroups.push(tmpGroup);
  }

  public handlerChengeTitle(value: { value: string, index: number }): void {
    this.todoGroups[value.index].title = value.value;
  }

  public handlerDeleteGroup(index: number): void {
    this.todoGroups.splice(index, 1);
  }

  public handlerNewItem(value: { item: TodoItem, index: number }): void {
    if (value.item.title != '')
      this.todoGroups[value.index].items.push(value.item);
  }

  public handleChangeDescription(value: { description: string, indexGroup: number, indexItem: number }) {
    this.todoGroups[value.indexGroup].items[value.indexItem].description = value.description;
  }

  public handleChangeStatus(value: { status: TodoStatus, indexItem: number, groupIndex: number }): void {
    this.todoGroups[value.groupIndex].items[value.indexItem].status = value.status;
  }

  public handleDeleteItem(value: { indexItem: number, indexGroup: number }): void {
    this.todoGroups[value.indexGroup].items.splice(value.indexItem, 1);
  }
}
