import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {AuthService} from "../../../services/auth.service";
import {ToastrService} from "ngx-toastr";
import {StorageKey} from "../../../models/enums/storage-key";
import StorageService from "../../../services/storage.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent extends StorageService{
  user = this.fb.group({
    email: ['', [Validators.required, Validators.minLength(8), Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8), Validators.pattern(/^(?=[^A-Z\s]*[A-Z])(?=[^a-z\s]*[a-z])(?=[^\d\s]*\d)(?=\w*[\W_])\S{8,}$/)]],
    firstName: ['', Validators.required],
    lastName:['', Validators.required],
    userName:['', [Validators.required, Validators.minLength(6)]],
    confirmPassword:['']
  });
  globalError: string | undefined;
  @Output() signIn: EventEmitter<boolean> = new EventEmitter<boolean>();
  hasCode = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {
    super();
  }

  updateConfirmPassword(newPassword: string) {
    this.user.get('confirmPassword')?.setValue(newPassword);
  }

  goToSignIn(e: Event): void {
    e.preventDefault();
    e.stopPropagation();
    this.signIn.emit(true);
  }
  
  getUserNameErrorMessage(): string {
    if (this.user?.get('userName')?.hasError('minlength')) {
        return 'Введіть коректний username';
    }
    if (this.user?.get('userName')?.hasError('required')) {
        return 'Введіть коректний username';
    }
    return '';
}

  getPasswordErrorMessage() {
  if (this.user.get('password')?.hasError('required')) {
      return "Пароль є обов'язковим для заповнення";
  } else if (this.user.get('password')?.hasError('pattern') || this.user.get('password')?.hasError('minlength')) {
    return 'Пароль занадто простий, мінімум 8 символів.\nДодайте принаймні:\n• одну букву у нижньому регістрі\n• одну букву у верхньому регістрі\n• одну цифру\n• один символ.\nБукви використовуйте латинські.';
  }else{
    return '';
  }
  }

getEmailErrorMessage() {
  if (this.user?.get('email')?.hasError('required')) {
      return "E-mail є обов'язковим для заповнення";
  } else if (this.user?.get('email')?.hasError('minlength')) {
      return 'Введіть коректний e-mail';
  }else{
    return '';
  }
}

getFirstNameErrorMessage() {
  if (this.user.get('firstName')?.hasError('required')) {
      return "Імя є обов'язковим для заповнення";
  } else if (this.user.get('firstName')?.hasError('minlength')) {
      return "Введіть коректне ім'я";
  }else{
    return '';
  }
}

getLastNameErrorMessage() {
  if (this.user.get('lastName')?.hasError('required')) {
      return "Прізвище є обов'язковим для заповнення";
  } else if (this.user.get('lastName')?.hasError('minlength')) {
      return "Введіть коректне прізвище";
  }else{
    return '';
  }
}



  registration({value, valid}: { value: any, valid: boolean }) {
    console.log(valid);
    this.globalError = '';
    if (valid) {
      this.authService.registration(value).subscribe(
        response => {
          this.toastr.success('Зареєстровано');
          this.setDataStorage(StorageKey.authToken, response);
          console.log(response.message);
        },
        error => {
          console.error("Server error:", error.error);
          this.globalError = 'Помилка при реєстрації. Спробуйте ще раз.';
          this.toastr.error(this.globalError);
        }
      );
    }
   }
}
