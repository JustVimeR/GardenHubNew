import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OrderStatus } from 'src/app/models/enums/order-status';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.scss']
})
export class PortfolioComponent {

  constructor(private router: Router){}

  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  viewOrderDetails(orderId: number) {
    
    this.router.navigateByUrl(`api/orders/order/${orderId}`);
  }

  createOrder() {
    this.router.navigateByUrl(`api/profilesettings/create-order`);
  }

  fakeOrders:any = [
    {
      id: 1,
      title: 'Покосити газон на прибудинковій території',
      location: 'м. Вишгород, Київська обл.',
      price: '700',
      isHeartClicked: false,
      typeOfWork: [
       'Догляд за газоном','Догляд за фруктовими деревами','Ландшафтний дизайн'
      ],
      orderStatus: OrderStatus.complited
    },
    {
      id: 2,
      title: 'Обрізка фруктових дерев у саду',
      location: 'м. Житомир',
      price: 'Договірна',
      isHeartClicked: false,
      typeOfWork: [
       'Догляд за фруктовими деревами','Ландшафтний дизайн','Догляд за газоном','Догляд за газоном'
      ],
      orderStatus: OrderStatus.complited
    }
  ]
}
