import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {SharedModule} from "../../shared.module";
import { MainPageComponent } from './main-page.component';
import { MatButtonModule } from '@angular/material/button';
import { MatTreeModule } from '@angular/material/tree';
import { IonicModule } from '@ionic/angular';

const mainPageRoutes = [
  {path: '', component: MainPageComponent}
]

@NgModule({
  declarations: [
    MainPageComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(mainPageRoutes),
    SharedModule,
    MatTreeModule,
    MatButtonModule,
    IonicModule.forRoot()
  ]
})
export class MainPageModule { }