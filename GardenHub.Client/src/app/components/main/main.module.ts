import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MainComponent} from './main.component';
import {RouterModule, Routes} from "@angular/router";
import {NavigationComponent} from "../general/navigation/navigation.component";
import {Path} from "../../models/enums/path";
import {SharedModule} from "../../shared.module";
import {ErrorPageComponent} from "../error-page/error-page.component";
import { StopRouteGuard } from '../../guards/stop-route.guard';


const mainRoutes: Routes = [
    {
      path: '', component: MainComponent,
      children: [
        {
          path: Path.mainPage,
          loadChildren: () => import('../main-page/main-page.module').then(m => m.MainPageModule),
          canActivate: [StopRouteGuard]
        },
        {
          path: Path.profileSettings,
          loadChildren: () => import('../profile-settings/profile-settings.module').then(m => m.ProfileSettingsModule),
          canActivate: [StopRouteGuard]
        },
        {
          path: Path.orders,
          loadChildren: () => import('../orders/orders.module').then(m => m.OrdersModule),
          canActivate: [StopRouteGuard]
        },
        {
          path: Path.profile,
          loadChildren: () => import('../my-profile/my-profile.module').then(m => m.MyProfileModule),
          canActivate: [StopRouteGuard]
        },
        {
          path: Path.notification,
          loadChildren: () => import('../notifications/notifications.module').then(m => m.NotificationsModule),
          canActivate: [StopRouteGuard]
        },
        {
          path: Path.createOrder,
          loadChildren: () => import('../create-order/create-order.module').then(m => m.CreateOrderModule),
          canActivate: [StopRouteGuard]
        },
        {path: Path.error, component: ErrorPageComponent},
        {path: '**', redirectTo: Path.mainPage}
      ]
    }
  ];
  
  @NgModule({
    declarations: [
      MainComponent,
      NavigationComponent
    ],
    imports: [
      CommonModule,
      RouterModule.forChild(mainRoutes),
      SharedModule
    ],
    exports: [NavigationComponent]
  })
  export class MainModule {
    
  }