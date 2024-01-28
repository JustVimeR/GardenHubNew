import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared.module';
import { RouterModule } from '@angular/router';
import { NotificationsComponent } from './notifications.component';

const notificationRoutes = [
  {path: '', component: NotificationsComponent},
]

@NgModule({
  declarations: [
    NotificationsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(notificationRoutes),
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class NotificationsModule { }
