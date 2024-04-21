import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-successfull-take-order',
  templateUrl: './successfull-take-order.component.html',
  styleUrls: ['./successfull-take-order.component.scss']
})
export class SuccessfullTakeOrderComponent {
  @Output() close = new EventEmitter<void>();
  
  onClose() {
    this.close.emit();
  }
}
