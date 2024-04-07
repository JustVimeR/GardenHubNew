import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit{
  @Input() messages: any;

  constructor(
    private chatService: ChatService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(){
  }

}
