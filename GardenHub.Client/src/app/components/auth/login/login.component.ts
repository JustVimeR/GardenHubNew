import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import StorageService from "../../../services/storage.service";
import {StorageKey} from "../../../models/enums/storage-key";
import { AuthService } from 'src/app/services/auth.service';
import { SettingsService } from 'src/app/services/settings.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends StorageService{
  user = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });
  globalError: string | undefined;
  @Output() signUp: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() forgotPassword: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private settingsService: SettingsService,
    private router: Router
  ) {
    super();
  }

  login({value, valid}: { value: any, valid: boolean }) {
    this.globalError = '';
    if (valid) {
      this.authService.login(value).subscribe(response => {
        if (response && response.data && response.data.jwToken) {
          this.setDataStorage(StorageKey.authToken, response.data.jwToken);
          this.router.navigate(['/api/main']);
        }
      });
      } else {
        this.globalError = 'Заповніть корректно поля';
      }
    }


  goToSignUp(e: Event): void {
    e.preventDefault();
    e.stopPropagation();
    this.signUp.emit(true);
  }

  goToResetPassword(): void {
    this.forgotPassword.emit(true);
  }
}
