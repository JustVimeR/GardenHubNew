import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-chat',
  templateUrl: './user-chat.component.html',
  styleUrls: ['./user-chat.component.scss']
})
export class UserChatComponent implements OnInit{

  unreadMessages = 50;

  constructor() { }

  ngOnInit(): void {
  }

  chats: any[] = [{
      name: 'Biba',
      description: 'Boba',
      id:0
    },{
      name: 'Biba1',
      description: 'Boba1',
      id:1
    },{
      name: 'Biba2',
      description: 'Boba2',
      id:2
    },{
      name: 'Biba3',
      description: 'Boba3',
      id:3
    },
  ]
}
