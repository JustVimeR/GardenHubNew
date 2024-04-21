import { Component } from '@angular/core';
import { LoaderService } from '../../../services/loader.service';

@Component({
  selector: 'app-loader',
  template: `
    <div *ngIf="isLoading$ | async" class="overlay">
      <mat-progress-spinner mode="indeterminate"></mat-progress-spinner>
    </div>
  `,
  styles: [`
    .overlay {
      position: fixed;
      display: flex;
      justify-content: center;
      align-items: center;
      top: 0;
      left: 0;
      height: 100%;
      width: 100%;
      background-color: rgba(0, 0, 0, 0.5);
      z-index: 1000;
    }
  `]
})
export class LoaderComponent {
  isLoading$ = this.loaderService.isLoading;

  constructor(private loaderService: LoaderService) {}
}
