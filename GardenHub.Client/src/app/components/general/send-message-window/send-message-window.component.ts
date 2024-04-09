import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-send-message-window',
  templateUrl: './send-message-window.component.html',
  styleUrls: ['./send-message-window.component.scss']
})
export class SendMessageWindowComponent {
  @Input() userProfile: any;
  @Output() close = new EventEmitter<void>();
  @Output() send = new EventEmitter<void>();
  @Output() messageSent = new EventEmitter<string>();

  messageText: string = '';

  onClose() {
    this.close.emit();
  }

  onSend() {
     if (!this.messageText.trim()) return;
      this.messageSent.emit(this.messageText); 
      this.messageText = '';
  }
}
