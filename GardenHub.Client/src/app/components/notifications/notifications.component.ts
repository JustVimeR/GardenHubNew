import { Component, NgZone, OnInit } from '@angular/core';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { ChatService } from 'src/app/services/chat.service';
import { ProjectService } from 'src/app/services/project.service';
import { SignalRService } from 'src/app/services/signalR.service';
import StorageService from 'src/app/services/storage.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent extends StorageService implements OnInit{

  notifications: any[] = [];

  constructor(
    private ngZone: NgZone,
    private storageService: StorageService,
    private signalRService: SignalRService,
    private chatService: ChatService,
    private projectService : ProjectService
  ) {
    super();
  }

  ngOnInit(): void {
    this.getNotifications();
    this.listenForProjectApplications();
  }

  getNotifications(): void {
    this.chatService.getNotifications().subscribe(
      response => {
        this.notifications = response.data || [];
        this.storageService.setDataStorage(StorageKey.notifications, this.notifications);
      },
      error => {
        console.error('Failed to fetch notifications:', error);
      }
    );
  }

  extractProjectId(message: string): string | null {
    const match = message.match(/projectId:\s*(\d+)/);
    return match ? match[1] : null;
  }

  listenForProjectApplications(): void {
    this.signalRService.projectApplyReceived.subscribe(({ userId, projectId, message }) => {
      const newNotification = {
        message: `projectId: ${projectId}: ${message}`,
        senderUserId: userId
      };
      this.ngZone.run(() => {
        this.notifications.push(newNotification);
        this.storageService.setDataStorage(StorageKey.notifications, this.notifications); 
      });
    });
  }

  confirmProject(notification: any): void {
    const projectId = this.extractProjectId(notification.message);
    if (projectId) {
      this.projectService.updateProject(projectId, 'InProggress').subscribe({
        next: (response) => {
          console.log('Project updated successfully', response);
          // Опционально обновить UI или уведомить пользователя
        },
        error: (error) => {
          console.error('Failed to update project', error);
          // Обработать ошибку
        }
      });
    } else {
      console.error('No project ID found in the message');
      // Сообщите пользователю, что ID проекта не найден
    }
  }
  
  

}
