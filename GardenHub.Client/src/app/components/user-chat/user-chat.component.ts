import { Component, OnInit } from '@angular/core';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { ChatService } from 'src/app/services/chat.service';
import { ResponsiveService } from 'src/app/services/responsive.service';
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
  isPhonePortrait: boolean;
  isBigScreen: boolean;
  chat: boolean;
  list: boolean = true;
  observe: any;

  constructor( 
    private signalRService: SignalRService,
    private chatService: ChatService,
    private responsiveService: ResponsiveService
  ) {
    super();
  }

  ngOnInit(): void {
    
    this.getChats();
    this.observe = this.responsiveService.checkScreenSize().subscribe(result => {
      if (result.matches) {
        console.log('Phone');
        this.isPhonePortrait = true;
        this.isBigScreen = false;
      } else {
        this.isBigScreen = true;
        this.isPhonePortrait = false;
      }
    });
  }

  selectChat(chatId: string): void {
    this.chatService.getChatById(chatId).subscribe(response => {
      this.selectedChat = response.data;
      if (this.selectedChat && this.selectedChat.chatMessages) {
        this.selectedChat.chatMessages = this.selectedChat.chatMessages.reverse();
      }
      this.setDataStorage(StorageKey.chatMessages, this.selectedChat);
    });
    console.log(this.selectedChat)
    this.changeView();
  }
  

  getChats(): void {
    this.chatService.getChats().subscribe(response => {
      this.chats = response;
      this.setDataStorage(StorageKey.chats, this.chats);
    });
  }

  changeView(){
    if(this.chat){
      this.chat = false;
      this.list = true;
    }
    else{
      this.chat = true;
      this.list = false;
    }
  }

  // apply()
  // {
  //   this.signalRService.sendProjectApplyNotification("notification test", "22");
  //   this.signalRService.sendChatMessage("chat test", "2");
  // }

}
