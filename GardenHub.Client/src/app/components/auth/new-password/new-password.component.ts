import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.scss']
})
export class NewPasswordComponent {

  password = this.fb.group({
    new_password: ['', [Validators.required, Validators.minLength(8)]],
    repeat_password: ['', [Validators.required, Validators.minLength(8)]]
  });

  passwordVisibility: any = {
    new_password: false,
    repeat_password: false
  };

  constructor(
    private fb: FormBuilder,
  ) {}

  togglePasswordVisibility(field: string) {
    this.passwordVisibility[field] = !this.passwordVisibility[field];
  }

  isPasswordVisible(field: string) {
    return this.passwordVisibility[field];
  }

  onSubmit() {
    if (this.password.valid) {
      this.password.get('new_password')!.value;
      this.password.get('repeat_password')!.value;
    }
  }
}
