import { HttpParams } from '@angular/common/http';
import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { ChatService } from 'src/app/services/chat.service';
import { ProjectService } from 'src/app/services/project.service';
import { SignalRService } from 'src/app/services/signalR.service';
import StorageService from 'src/app/services/storage.service';
import { UserProfileService } from 'src/app/services/user-profile.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent extends StorageService implements OnInit{

  notifications: any[] = [];
  userProfiles: { [key: string]: any } = {};

  constructor(
    private ngZone: NgZone,
    private storageService: StorageService,
    private signalRService: SignalRService,
    private chatService: ChatService,
    private projectService : ProjectService,
    private userProfileService: UserProfileService,
    private router: Router,
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
        this.notifications.forEach(notification => {
          this.loadUserProfile(notification.senderUserId);
        });
        this.storageService.setDataStorage(StorageKey.notifications, this.notifications);
      },
      error => {
        console.error('Failed to fetch notifications:', error);
      }
    );
  }

  loadUserProfile(userId: string): void {
    if (!this.userProfiles[userId]) {
      this.userProfileService.getUserProfileById(userId).subscribe(
        userProfile => {
          this.ngZone.run(() => {
            this.userProfiles[userId] = userProfile.data.userName;
          });
        },
        error => {
          console.error('Error fetching user profile:', error);
        }
      );
    }
  }

  onConfirm(notification: any): void {
    const projectId = this.extractProjectId(notification.message);
    const gardenerId = notification.senderUserId; 

    if (!projectId) return;

    this.projectService.getProjectById(projectId).subscribe({
      next: project => {
        const updatedProject = {
          title: project.data.title,
          location: project.data.location,
          budget: project.data.budget,
          description: project.data.description,
          numberOfRequriedGardeners: 1,
          status: 1,
          startDate: project.data.startDate,
          endDate: project.data.endDate,
          workTypes: project.data.workTypes
        };

        this.projectService.updateProject(projectId, updatedProject).subscribe({
          next: response => {
            console.log('Project updated successfully:', response);
            this.getProjectAccept(projectId, gardenerId);
          },
          error: error => {
            console.error('Error updating project:', error);
          }
        });
      },
      error: error => {
        console.error('Error retrieving project:', error);
      }
    });
}

private getProjectAccept(projectId: string, gardenerId: string): void {
    const params = new HttpParams()
      .set('projectId', projectId)
      .set('gardenerId', gardenerId);

    this.chatService.getProjectAccept(params).subscribe({
      next: (response) => {
        console.log('Project acceptance retrieved successfully:', response);
      },
      error: (error) => {
        console.error('Error fetching project acceptance:', error);
      }
    });
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

  viewAuthor(userId: string) {
    if (userId) {
      this.router.navigateByUrl(`/api/homeowner-profile/${userId}`);
    } else {
      console.error('User ID is missing');
    }
  }

}
