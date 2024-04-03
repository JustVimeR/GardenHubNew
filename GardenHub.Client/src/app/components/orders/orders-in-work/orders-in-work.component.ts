import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { OrderStatus } from 'src/app/models/enums/order-status';

@Component({
  selector: 'app-orders-in-work',
  templateUrl: './orders-in-work.component.html',
  styleUrls: ['./orders-in-work.component.scss']
})
export class OrdersInWorkComponent {

  constructor(private router: Router){}
  @Input() allProjects: any;
  
  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  viewOrderDetails(orderId: number) {
    this.router.navigateByUrl(`api/orders/order/${orderId}`);
  }

  getProggressProjects() {
    if (!this.allProjects.data) {
      return [];
    }
    return this.allProjects.data.filter((order: any) => order.status === 'InProggress');
  }
}
