import { Component, Input } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';

@Component({
  selector: 'app-orders-complited',
  templateUrl: './orders-complited.component.html',
  styleUrls: ['./orders-complited.component.scss']
})
export class OrdersComplitedComponent {
  @Input() allProjects: any;
  
  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  getCompletedProjects() {
    if (!this.allProjects.data) {
      return [];
    }
    return this.allProjects.data.filter((order: any) => order.status === 'Completed');
  }

}
