import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-successfull-create-order',
  templateUrl: './successfull-create-order.component.html',
  styleUrls: ['./successfull-create-order.component.scss']
})
export class SuccessfullCreateOrderComponent {
  @Output() close = new EventEmitter<void>();
  
  onClose() {
    this.close.emit();
  }
}
