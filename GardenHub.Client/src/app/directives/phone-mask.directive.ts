import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[phoneMask]'
})
export class PhoneMaskDirective {
  private readonly prefix = '38';
  private readonly maxLength = 12;

  constructor(private el: ElementRef) { }

  @HostListener('input', ['$event'])
  onInput(event: InputEvent): void {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, '');

    if (value.startsWith(this.prefix)) {
      value = this.prefix + value.slice(this.prefix.length, this.maxLength);
    } else {
      value = this.prefix;
    }

    input.value = value;
  }

  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent): void {
    if (!this.isAllowedKey(event.key) && !this.isSpecialKey(event)) {
      event.preventDefault();
    }
  }

  private isAllowedKey(key: string): boolean {
    return /^\d$/.test(key);
  }

  private isSpecialKey(event: KeyboardEvent): boolean {
    return event.ctrlKey || event.shiftKey || event.altKey ||
      event.key === 'Backspace' || event.key === 'Delete' ||
      event.key === 'ArrowLeft' || event.key === 'ArrowRight';
  }
}
