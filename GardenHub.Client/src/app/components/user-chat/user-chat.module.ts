import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {SharedModule} from "../../shared.module";
import { IonicModule } from '@ionic/angular';
import { UserChatComponent } from './user-chat.component';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { ChatInListComponent } from './chat-in-list/chat-in-list.component';
import { ChatComponent } from './chat/chat.component';

const userChatRoutes = [
  {path: '', component: UserChatComponent},
  { path: 'chat/:id', component: ChatComponent },
]

@NgModule({
  declarations: [
    UserChatComponent,
    ChatInListComponent,
    ChatComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(userChatRoutes),
    SharedModule,
    IonicModule.forRoot(),
    MatButtonModule,
    MatChipsModule,
  ]
})
export class UserChatModule { }