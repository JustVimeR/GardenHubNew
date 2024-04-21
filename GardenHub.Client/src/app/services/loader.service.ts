import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private requestCount = 0;
  public isLoading: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public onRequestStarted(): void {
    this.requestCount++;
    this.updateLoading();
  }

  public onRequestFinished(): void {
    this.requestCount--;
    this.updateLoading();
  }

  private updateLoading(): void {
    this.isLoading.next(this.requestCount > 0);
  }
}
