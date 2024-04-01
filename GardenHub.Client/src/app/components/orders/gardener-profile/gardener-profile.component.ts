import { Component } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';

@Component({
  selector: 'app-gardener-profile',
  templateUrl: './gardener-profile.component.html',
  styleUrls: ['./gardener-profile.component.scss']
})
export class GardenerProfileComponent {
  
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
      orderStatus: OrderStatus.complited
    }
  ]
}
