import { Component } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';
import StorageService from 'src/app/services/storage.service';
import { UserProfileService } from 'src/app/services/user-profile.service';
import { OnInit } from '@angular/core';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { FeedbackService } from 'src/app/services/feedback.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent extends StorageService implements OnInit{
  
  selfUserProfile: any = {};
  selfFeedback: any = {};

  constructor(
    private userProfileService: UserProfileService,
    private feedbackService: FeedbackService
    ) {
    super();
  }
 

  ngOnInit() {
    this.getUserProfile();
    this.getFeedback();
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

  getFeedback(): void {
    if (this.hasKeyInStorage(StorageKey.feedback)) {
      this.selfFeedback = this.getDataStorage(StorageKey.feedback);
    } else {
      this.feedbackService.getFeedback().subscribe(response => {
        this.selfFeedback = response;
        this.setDataStorage(StorageKey.feedback, this.selfFeedback);
      });
    }
  }

  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  getCompletedProjects() {
    if (!this.selfUserProfile.data.gardenerProjects) {
      return [];
    }
    return this.selfUserProfile.data.gardenerProjects.filter((order: any) => order.status === 'Completed');
  }
  
}
