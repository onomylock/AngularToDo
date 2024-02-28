import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ItemInProgressComponent } from '../item-in-progress/item-in-progress.component';
import { ItemDoneComponent } from '../item-done/item-done.component';
import { ItemNotStartedComponent } from '../item-not-started/item-not-started.component';

@Component({
  selector: 'app-item-factory',
  standalone: true,
  imports: [CommonModule, ItemInProgressComponent,
    ItemDoneComponent, ItemNotStartedComponent],
  templateUrl: './item-factory.component.html',
  styleUrl: './item-factory.component.scss'
})
export class ItemFactoryComponent {

}
