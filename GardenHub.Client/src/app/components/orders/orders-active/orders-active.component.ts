import { Component } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';

@Component({
  selector: 'app-orders-active',
  templateUrl: './orders-active.component.html',
  styleUrls: ['./orders-active.component.scss']
})
export class OrdersActiveComponent {
  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  fakeOrders:any = [
    {
      title: 'Покосити газон на прибудинковій території',
      location: 'м. Вишгород, Київська обл.',
      price: '700',
      isHeartClicked: false,
      typeOfWork: [
       'Догляд за газоном','Догляд за фруктовими деревами','Ландшафтний дизайн'
      ],
      orderStatus: OrderStatus.active
    },
    {
      title: 'Обрізка фруктових дерев у саду',
      location: 'м. Житомир',
      price: 'Договірна',
      isHeartClicked: false,
      typeOfWork: [
       'Догляд за фруктовими деревами','Ландшафтний дизайн','Догляд за газоном','Догляд за газоном'
      ],
      orderStatus: OrderStatus.active
    }
  ]
}
