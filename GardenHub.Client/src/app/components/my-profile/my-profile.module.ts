import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyProfileComponent } from './my-profile.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared.module';
import { IonicModule } from '@ionic/angular';

const myProfileRoutes = [
  {path: '', component: MyProfileComponent}
]

@NgModule({
  declarations: [
    MyProfileComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(myProfileRoutes),
    SharedModule,
    IonicModule
  ]
})
export class MyProfileModule { }
