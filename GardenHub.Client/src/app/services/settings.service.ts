import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SharedService} from "./shared.service";
import {Subject} from "rxjs";
import StorageService from "./storage.service";

@Injectable({
  providedIn: 'root'
})
export class SettingsService extends StorageService {
  API_URL = this.sharedService.API_URL;
  private showWindowSubject: Subject<boolean> = new Subject<boolean>();
  showWindow = this.showWindowSubject.asObservable();

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }

  show(): void {
    this.showWindowSubject.next(true);
  }

  hide(): void {
    this.showWindowSubject.next(false);
  }
}
