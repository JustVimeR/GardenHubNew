import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-successfull-update-profile',
  templateUrl: './successfull-update-profile.component.html',
  styleUrls: ['./successfull-update-profile.component.scss']
})
export class SuccessfullUpdateProfileComponent {
  @Output() close = new EventEmitter<void>();
  
  onClose() {
    this.close.emit();
  }
}
