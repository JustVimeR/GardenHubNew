import { Component, OnInit } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from 'src/app/services/user-profile.service';
import StorageService from 'src/app/services/storage.service';
import { StorageKey } from 'src/app/models/enums/storage-key';

@Component({
  selector: 'app-homeowner-profile',
  templateUrl: './homeowner-profile.component.html',
  styleUrls: ['./homeowner-profile.component.scss']
})
export class HomeownerProfileComponent extends StorageService implements OnInit{
  homeOwnerUserProfile: any = {};

  constructor(
    private route: ActivatedRoute,
    private userProfileService: UserProfileService
    ) {
      super();
    }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.getUserProfileById(id);
    });
  }

  getUserProfileById(id: string): void {
    const storedProfile = this.getDataStorage(StorageKey.homeOwnerProfile);
    if (storedProfile && storedProfile.id === id) {
      this.homeOwnerUserProfile = storedProfile;
    } else {
      this.userProfileService.getUserProfileById(id).subscribe(response => {
        this.homeOwnerUserProfile = response;
        this.setDataStorage(StorageKey.homeOwnerProfile, this.homeOwnerUserProfile);
      }, error => {
        console.error("Failed to fetch user profile:", error);
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
}
