import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-change-image-window',
  templateUrl: './change-image-window.component.html',
  styleUrls: ['./change-image-window.component.scss']
})
export class ChangeImageWindowComponent {
  @Output() close = new EventEmitter<void>();
  @Output() imageChanged = new EventEmitter<string>();

  imageUrl: string = '';

  onImageChange() {
    this.imageChanged.emit(this.imageUrl);
    this.onClose();
  }

  onClose() {
    this.close.emit();
  }
}
