import { Component, OnInit } from '@angular/core';
import { OrderStatus } from 'src/app/models/enums/order-status';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from 'src/app/services/user-profile.service';
import StorageService from 'src/app/services/storage.service';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { SignalRService } from 'src/app/services/signalR.service';

@Component({
  selector: 'app-homeowner-profile',
  templateUrl: './homeowner-profile.component.html',
  styleUrls: ['./homeowner-profile.component.scss']
})
export class HomeownerProfileComponent extends StorageService implements OnInit{
  homeOwnerUserProfile: any = {};
  cancelSend = false;
  messageText: string = '';

  constructor(
    private route: ActivatedRoute,
    private userProfileService: UserProfileService,
    private signalRService: SignalRService
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

  sendMessage(): void {
    if (!this.messageText.trim()) return;
  
    const receiverId = this.homeOwnerUserProfile.data.id;
    if (receiverId) {
      this.signalRService.sendChatMessage(this.messageText, receiverId.toString());

      this.messageText = '';
    } else {
      console.error('Receiver ID is not defined.'); 
    }
  }

  receiveMessageText(messageText: string): void {
    this.messageText = messageText;
    this.sendMessage();
  }

}
