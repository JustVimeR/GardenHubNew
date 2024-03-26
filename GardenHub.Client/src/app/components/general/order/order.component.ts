import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { OrderStatus } from 'src/app/models/enums/order-status';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent {
  @Input() order: any;
  @Output() heartToggle = new EventEmitter<any>();
  @Output() viewDetails = new EventEmitter<number>();

  OrderStatus = OrderStatus;

  constructor(private router: Router) {}

  toggleHeart() {
    this.heartToggle.emit(this.order);
  }

  viewOrderDetails() {
    this.router.navigate(['/api/order-details', this.order.id]);
  }

  translateStatus(status: string): string {
    const statusTranslations: {[key: string]: string} = {
      'Active': 'Активно',
      'InProggress': 'В роботі',
      'Completed': 'Виконано',
      'Inactive': 'Не активно'
    };

    return statusTranslations[status] || status;
  }
}
