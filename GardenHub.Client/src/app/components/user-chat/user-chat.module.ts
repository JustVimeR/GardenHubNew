import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {SharedModule} from "../../shared.module";
import { IonicModule } from '@ionic/angular';
import { UserChatComponent } from './user-chat.component';

const userChatRoutes = [
  {path: '', component: UserChatComponent},
  { path: ':id', component: UserChatComponent }
]

@NgModule({
  declarations: [
    UserChatComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(userChatRoutes),
    SharedModule,
    IonicModule.forRoot()
  ]
})
export class UserChatModule { }