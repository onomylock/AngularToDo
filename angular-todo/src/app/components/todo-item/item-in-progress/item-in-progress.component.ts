import { Component } from '@angular/core';
import { ItemBaseDirective } from '../item-base/item-base.directive';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-item-in-progress',
    imports: [CommonModule, FormsModule],
    templateUrl: './item-in-progress.component.html',
    styleUrl: './item-in-progress.component.scss'
})
export class ItemInProgressComponent extends ItemBaseDirective {

}
