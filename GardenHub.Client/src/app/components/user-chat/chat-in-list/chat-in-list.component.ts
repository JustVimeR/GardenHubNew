import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-chat-in-list',
  templateUrl: './chat-in-list.component.html',
  styleUrls: ['./chat-in-list.component.scss']
})
export class ChatInListComponent {
  @Input() chats: any;

  constructor(private router: Router) {}
  
  goToChat(id: number) {
    this.router.navigate(['/chat', id]);
  }
}
