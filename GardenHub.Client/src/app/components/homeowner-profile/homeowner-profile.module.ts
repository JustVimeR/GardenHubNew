import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {SharedModule} from "../../shared.module";
import { IonicModule } from '@ionic/angular';
import { HomeownerProfileComponent } from './homeowner-profile.component';

const homeownerProfileRoutes = [
  {path: '', component: HomeownerProfileComponent}
]

@NgModule({
  declarations: [
    HomeownerProfileComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(homeownerProfileRoutes),
    SharedModule,
    IonicModule.forRoot()
  ]
})
export class HomeOwnerProfileModule { }