import { Component,Input } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-chat-in-list',
  templateUrl: './chat-in-list.component.html',
  styleUrls: ['./chat-in-list.component.scss']
})
export class ChatInListComponent {
  @Input() chat: any;

  constructor() {}
  
  
}
