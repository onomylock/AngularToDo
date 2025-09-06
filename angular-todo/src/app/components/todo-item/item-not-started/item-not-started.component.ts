import { Component } from '@angular/core';
import { ItemBaseDirective } from '../item-base/item-base.directive';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-item-not-started',
    imports: [CommonModule, FormsModule],
    templateUrl: './item-not-started.component.html',
    styleUrl: './item-not-started.component.scss'
})
export class ItemNotStartedComponent extends ItemBaseDirective {

}
