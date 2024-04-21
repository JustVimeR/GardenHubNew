import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {provideToastr, ToastrModule} from "ngx-toastr";
import {provideAnimations, BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {SharedModule} from "./shared.module";
import {MAT_DATE_LOCALE} from "@angular/material/core";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthModule } from './components/auth/auth.module';
import { IonicModule } from '@ionic/angular';
import { AuthInterceptor } from './services/auth.interceptor.service';
import { UserProfileService } from './services/user-profile.service';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { LoaderComponent } from './components/general/loader/loader.component';

@NgModule({
  declarations: [
    LoaderComponent,
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    SharedModule,
    BrowserAnimationsModule,
    IonicModule,
    MatProgressSpinnerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    { 
      provide: HTTP_INTERCEPTORS, 
      useClass: LoaderInterceptor,
      multi: true 
    },
    provideAnimations(),
    provideToastr(),
    {provide: MAT_DATE_LOCALE, useValue: 'uk'},],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
