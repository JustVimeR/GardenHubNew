import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-restore-password',
  templateUrl: './restore-password.component.html',
  styleUrls: ['./restore-password.component.scss']
})
export class RestorePasswordComponent {
  @Output() back: EventEmitter<boolean> = new EventEmitter<boolean>();

  emailSent: boolean = false;

  restoreForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
  });

  constructor(
    private fb: FormBuilder,
  ) {}

  restorePassword() {
    if (this.restoreForm.valid) {
      this.emailSent = true;
    }
  }

  goToLogin() {
    this.back.emit(true);
  }
}
