import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared.module';
import { OrdersComponent } from './orders/orders.component';
import { OrdersComplitedComponent } from './orders-complited/orders-complited.component';
import { OrdersInWorkComponent } from './orders-in-work/orders-in-work.component';
import { RouterModule } from '@angular/router';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrdersActiveComponent } from './orders-active/orders-active.component';
import { GardenerProfileComponent } from './gardener-profile/gardener-profile.component';

const OrdersRoutes = [
  {path: '', component: OrdersComponent},
  {path: 'order/:id', component: OrderDetailsComponent},
  {path: 'order', component: OrderDetailsComponent},
  {path: 'gardener-profile/:id', component: GardenerProfileComponent},
  {path: 'gardener-profile', component: GardenerProfileComponent}
]

@NgModule({
  declarations: [
    OrdersComponent,
    OrdersComplitedComponent,
    OrdersInWorkComponent,
    OrderDetailsComponent,
    OrdersActiveComponent,
    GardenerProfileComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(OrdersRoutes),
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class OrdersModule { }
