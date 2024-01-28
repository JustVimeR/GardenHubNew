import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared.module';
import { RouterModule } from '@angular/router';
import { CreateOrderComponent } from './create-order.component';

const CreateOrderRoutes = [
  {path: '', component: CreateOrderComponent},
]

@NgModule({
  declarations: [
    CreateOrderComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(CreateOrderRoutes),
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class CreateOrderModule { }
