import {Component} from '@angular/core';
import { ItemBaseDirective } from '../item-base/item-base.directive';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-item-done',
    imports: [CommonModule, FormsModule],
    templateUrl: './item-done.component.html',
    styleUrl: './item-done.component.scss'
})
export class ItemDoneComponent extends ItemBaseDirective {

}
