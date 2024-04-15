import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { SharedService } from './shared.service';
import StorageService  from './storage.service';
import { StorageKey } from '../models/enums/storage-key';


@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  public messageReceived = new EventEmitter<{ userId: string, message: string }>();
  public projectApplyReceived = new EventEmitter<{ userId: string, projectId: number, message: string }>();

  static readonly AvailableActions = {
    ProjectApply: 'SendProjectApply',
    ChatMessage: 'SendMessage'
  };

  private chatsConnection: signalR.HubConnection;
  private notificationsConnection: signalR.HubConnection;
  private hubsUrl: string;

  constructor(
    private sharedService: SharedService, 
    private storageService: StorageService) {
    this.hubsUrl = sharedService.DOMAIN_URL + '/hubs';
    let token = storageService.getStringStorage(StorageKey.authToken);

    this.chatsConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubsUrl + '/chats', { accessTokenFactory: () => token, })
      .withAutomaticReconnect()
      .build();

    this.notificationsConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubsUrl + '/notifications', { accessTokenFactory: () => token })
      .withAutomaticReconnect()
      .build();

    this.startConnection(this.chatsConnection);
    this.startConnection(this.notificationsConnection);

    this.notificationsConnection.on("ReceiveProjectApply", (userId: string, projectId: number, message: string) => {
      this.projectApplyReceived.emit({ userId, projectId, message });
      console.log(`Received project application from user ${userId} for project ${projectId}: ${message}`);
    });

    this.chatsConnection.on("ReceiveMessage", (userId: string, message: string) => {
      this.messageReceived.emit({ userId, message });
      console.log(`Received message from user ${userId}: ${message}`);
  });
  }

  private startConnection(hubConnection: signalR.HubConnection) {
    hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  sendMessage(connection: signalR.HubConnection, action: string, message: string, receiverId: string) {
    if (connection.state !== signalR.HubConnectionState.Connected) {
      connection.start()
        .then(() => connection.invoke(action, message, receiverId))
        .catch(err => console.error('Error while starting connection or sending message: ' + err));
    } else {
      connection.invoke(action, message, receiverId)
        .catch(err => console.error('Error while sending message: ' + err));
    }
  }

  sendProjectApplyNotification(message: string,  projectId: string) {
    this.sendMessage(this.notificationsConnection, SignalRService.AvailableActions.ProjectApply, message, projectId);
  }

  sendChatMessage(message: string,  receiverId: string) {
    this.sendMessage(this.chatsConnection, SignalRService.AvailableActions.ChatMessage, message, receiverId);
  }

}