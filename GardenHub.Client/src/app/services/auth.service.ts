import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Router} from "@angular/router";
import {SharedService} from "./shared.service";
import StorageService from "./storage.service";
import {StorageKey} from "../models/enums/storage-key";

@Injectable({
  providedIn: 'root'
})
export class AuthService extends StorageService {
  API_URL = this.sharedService.API_URL;

  constructor(
    private http: HttpClient,
    private router: Router,
    private sharedService: SharedService
  ) {
    super();
  }

  setToken(token: string, user?: object): void {
    this.setStringStorage(StorageKey.authToken, token);
    this.navigateTo('api');
  }

  resetToken(): void {
    this.removeDataStorage(StorageKey.authToken);
    this.navigateTo('auth');
  }

  navigateTo(path: string): void {
    this.router.navigateByUrl(path);
  }

  login(body: object): Observable<any> {
    return this.http.post(`${this.API_URL}/Account/authenticate`, body, {});
  }

  logout(): Observable<any> {
    return this.http.get(`${this.API_URL}/Account/logout`, {});
  }

  registration(body: object): Observable<any> {
    return this.http.post(`${this.API_URL}/Account/register`, body, {});
  }

  confirmRegistration(body: object): Observable<any> {
    return this.http.get(`${this.API_URL}/Account/confirm-email`, {});
  }

  forgotPassword(body: object): Observable<any> {
    return this.http.post(`${this.API_URL}/Account/forgot-passowrd`, body, {});
  }

  resetPassword(body: object): Observable<any> {
    return this.http.post(`${this.API_URL}/Account/reset-passowrd`, body, {});
  }

  refreshtoken(body: object): Observable<any> {
    return this.http.post(`${this.API_URL}/Account/refreshtoken`, body, {});
  }
}
