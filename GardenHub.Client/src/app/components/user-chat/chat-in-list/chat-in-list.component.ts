import { Component,Input } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-chat-in-list',
  templateUrl: './chat-in-list.component.html',
  styleUrls: ['./chat-in-list.component.scss']
})
export class ChatInListComponent {
  @Input() chat: any;
  fakeImg='../../../../assets/user-chat.svg';

  imageHandler () {
    if(!this.chat.interlocutorProfile.icon.url){
      return this.fakeImg;
    } 
    return this.chat.interlocutorProfile.icon.url;
    
  } 

  constructor() {}
  
  
}
