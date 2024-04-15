import { Component, OnInit } from '@angular/core';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { ChatService } from 'src/app/services/chat.service';
import { SignalRService } from 'src/app/services/signalR.service';
import StorageService from 'src/app/services/storage.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent extends StorageService implements OnInit{

  notifications: any = {};

  constructor(
    private storageService: StorageService,
    private signalRService: SignalRService,
    private chatService: ChatService
  ) {
    super();
  }

  ngOnInit(): void {
    this.getNotifications();
    // this.listenForProjectApplications();

    this.signalRService.projectApplyReceived.subscribe(({ userId, projectId, message }) => {
      console.log(`Received from user ${userId}: ${message}`);
      const newNotification = {
        message: `Нова заявка на проект #${projectId}: ${message}`,
        senderUserId: userId
      };
      this.notifications.push(newNotification);
    });
  }

  getNotifications(): void {
    if (this.hasKeyInStorage(StorageKey.notifications)) {
      this.notifications = this.getDataStorage(StorageKey.notifications);
    } else {
      this.chatService.getNotifications().subscribe(response => {
        this.notifications = response;
        this.setDataStorage(StorageKey.notifications, this.notifications);
      });
    }
  }

  // listenForProjectApplications(): void {
  //   this.signalRService.projectApplyReceived.subscribe(({ userId, projectId, message }) => {
  //     const newNotification = {
  //       message: `Нова заявка на проект #${projectId}: ${message}`,
  //       senderUserId: userId
  //     };
  //     this.notifications.push(newNotification);
  //   });
  // }
}
