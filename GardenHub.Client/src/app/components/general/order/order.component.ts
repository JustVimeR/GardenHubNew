import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent {
  @Input() order: any;
  @Output() heartToggle = new EventEmitter<any>();
  @Output() viewDetails = new EventEmitter<number>();

  toggleHeart() {
    this.heartToggle.emit(this.order);
  }

  viewOrderDetails() {
    this.viewDetails.emit(this.order.id);
  }
}
