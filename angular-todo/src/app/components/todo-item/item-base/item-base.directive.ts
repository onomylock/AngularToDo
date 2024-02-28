import { Directive, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TodoItem, TodoStatus } from '../../../interfaces/todo-group.interface';
import { FormControl } from '@angular/forms';

@Directive({
  selector: '[appItemBase]',
  standalone: true
})
export class ItemBaseDirective implements OnInit {
  @Input() todoItem!: TodoItem;
  @Input() index!: number;

  @Output() eventChangeDescription = new EventEmitter<{ description: string, index: number }>();
  @Output() changeStatus = new EventEmitter<{ status: TodoStatus, index: number }>()
  @Output() deleteItem = new EventEmitter<number>()

  public isShowDescriptions = false;

  public description?: string;

  public todoStatus = TodoStatus;

  ngOnInit(): void {
    this.description = this.todoItem.description;
  }

  public emitChangeDescription(value: string): void {
    this.eventChangeDescription.emit({ description: value, index: this.index });
  }

  public emitChangeStatus(value: TodoStatus): void {
    this.changeStatus.emit({ status: value, index: this.index })
  }

  public emitDeleteItem(): void {
    this.deleteItem.emit(this.index)
  }
}
