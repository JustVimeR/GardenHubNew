import { Component, OnDestroy } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';
import StorageService from 'src/app/services/storage.service';
import { UserProfileService } from 'src/app/services/user-profile.service';
import { OnInit } from '@angular/core';
import { StorageKey } from 'src/app/models/enums/storage-key';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent extends StorageService implements OnInit, OnDestroy{
  
  selfUserProfile: any = {};

  constructor(
    private userProfileService: UserProfileService) {
    super();
  }
 

  ngOnInit() {
    this.getUserProfile();
  }

  getUserProfile(): void {
    if (this.hasKeyInStorage(StorageKey.userProfile)) {
      this.selfUserProfile = this.getDataStorage(StorageKey.userProfile);
    } else {
      this.userProfileService.getSelfUserProfile().subscribe(response => {
        this.selfUserProfile = response;
        this.setDataStorage(StorageKey.userProfile, this.selfUserProfile);
      });
    }
  }

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
    },
    {
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

  fakeReviews:any = [
    {
      username: 'alexpop',
      text: 'Чудово виконана робота, рекомендую цього садівника! Всі побажання врахувала, правильно і красиво обрала місця для насаджень дерев. ',
      reviewRoute: 'Створення фруктового саду',
      data: '01.07.2023',
      rate: 5
    },
    {
      username: 'olenaelas',
      text: 'Дякую за роботу! Усе сподобалось, проте з одним деревом вийшло трохи не те, що очікувалось, тому потрібно чекати поки відросте знову.',
      reviewRoute: 'Кронування декоративних дерев',
      data: '31.07.2023',
      rate: 4
    }
  ]
  ngOnDestroy(): void {
    if (this.selfUserProfile) {
      this.selfUserProfile.unsubscribe();
    }
  }
}
