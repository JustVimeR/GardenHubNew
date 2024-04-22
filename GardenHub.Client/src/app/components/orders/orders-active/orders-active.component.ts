import { Component, Input } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';

@Component({
  selector: 'app-orders-active',
  templateUrl: './orders-active.component.html',
  styleUrls: ['./orders-active.component.scss']
})
export class OrdersActiveComponent {
  @Input() allProjects: any;

  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  getActiveProjects() {
    if (!this.allProjects?.data?.customerProjects) {
      return [];
    }
    return this.allProjects?.data?.customerProjects.filter((order: any) => order.status === 'Active');
  }
}
