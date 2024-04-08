import { Component, OnInit } from '@angular/core';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { ChatService } from 'src/app/services/chat.service';
import { SignalRService } from 'src/app/services/signalR.service';
import StorageService from 'src/app/services/storage.service';

@Component({
  selector: 'app-user-chat',
  templateUrl: './user-chat.component.html',
  styleUrls: ['./user-chat.component.scss']
})
export class UserChatComponent extends StorageService implements OnInit{
  unreadMessages = 50;
  chats: any = {};
  selectedChat: any = null;

  constructor( 
    private signalRService: SignalRService,
    private chatService: ChatService
  ) {
    super();
  }

  ngOnInit(): void {
    this.getChats();
  }

  selectChat(chatId: string): void {
    this.chatService.getChatById(chatId).subscribe(response => {
      this.selectedChat = response.data;
      if (this.selectedChat && this.selectedChat.chatMessages) {
        this.selectedChat.chatMessages = this.selectedChat.chatMessages.reverse();
      }
      this.setDataStorage(StorageKey.chatMessages, this.selectedChat);
    });
  }
  

  getChats(): void {
    if (this.hasKeyInStorage(StorageKey.chats)) {
      this.chats = this.getDataStorage(StorageKey.chats);
    } else {
      this.chatService.getChats().subscribe(response => {
        this.chats = response;
        this.setDataStorage(StorageKey.chats, this.chats);
      });
    }
  }

  // apply()
  // {
  //   this.signalRService.sendProjectApplyNotification("notification test", "22");
  //   this.signalRService.sendChatMessage("chat test", "2");
  // }

}
