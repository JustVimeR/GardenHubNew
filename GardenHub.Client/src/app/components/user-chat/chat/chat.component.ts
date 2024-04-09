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
  fakeImg='../../../../../assets/user-chat.svg';

  constructor(
    private storageService: StorageService,
    private signalRService: SignalRService
  ) {
    super();
  }

  ngOnInit(){
    this.signalRService.messageReceived.subscribe((message: any) => {
      // console.log(`Received from user ${message.userId}: ${message.message}`);
      this.selectedChat.chatMessages.push({
        message: message.message,
        senderUserId: message.userId,
        publicationDate: new Date().toISOString()
    });
    });
  }

  isCurrentUser(senderUserId: number): boolean {
    const userProfile = this.storageService.getDataStorage(StorageKey.userProfile);
    return userProfile.data?.id === senderUserId;
  }

  sendMessage(): void {
    if (!this.messageText.trim()) return;
  
    const receiverId = this.selectedChat?.interlocutorProfile?.id;
    if (receiverId) {
      this.signalRService.sendChatMessage(this.messageText, receiverId.toString());
      
      const fakeMessage = {
        message: this.messageText,
        senderUserId: this.storageService.getDataStorage(StorageKey.userProfile).data?.id,
        publicationDate: new Date().toISOString()
      };
  
      if (!this.selectedChat.chatMessages) {
        this.selectedChat.chatMessages = [];
      }
      this.selectedChat.chatMessages.push(fakeMessage);
  
      this.messageText = '';
    } else {
      console.error('Receiver ID is not defined.'); 
    }
  }
  

  imageHandler () {
    if(!this.selectedChat?.interlocutorProfile?.icon?.url){
      return this.fakeImg;
    } 
    return this.selectedChat?.interlocutorProfile?.icon?.url;
    
  } 

}
