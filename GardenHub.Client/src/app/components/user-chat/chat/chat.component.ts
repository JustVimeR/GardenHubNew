import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { ChatService } from 'src/app/services/chat.service';
import { SignalRService } from 'src/app/services/signalR.service';
import StorageService from 'src/app/services/storage.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent extends StorageService implements OnInit{
  @Input() selectedChat: any;
  @Input() chat: any;
  messageText: string = '';
  fakeImg='../../../../assets/user-chat.svg';
  
  constructor(
    private storageService: StorageService,
    private signalRService: SignalRService
  ) {
    super();
  }

  ngOnInit(){
    
  }

  isCurrentUser(senderUserId: number): boolean {
    const userProfile = this.storageService.getDataStorage(StorageKey.userProfile);
    return userProfile.data?.id === senderUserId;
  }

  sendMessage(): void {
    if (!this.messageText.trim()) return;

    const receiverId = 1; // тут мінять ІД
    if (receiverId) {
      this.signalRService.sendChatMessage(this.messageText, receiverId.toString());
      this.messageText = '';
    } else {
      console.error('Receiver ID is not defined.'); 
    }
  }

}
