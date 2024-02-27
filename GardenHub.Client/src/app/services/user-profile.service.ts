import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {SharedService} from "./shared.service";
import StorageService from "./storage.service";

@Injectable({
  providedIn: 'root'
})
export class UserProfileService extends StorageService {
  API_URL = this.sharedService.API_URL;
  private userProfileSubject: BehaviorSubject<any | null> = new BehaviorSubject<any| null>(null);
  userProfile$ = this.userProfileSubject.asObservable();
  canRoute = true;

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }

  setUserProfile(user: any): void {
    this.userProfileSubject.next(user);
  }

  resetUserProfile(): void {
    this.userProfileSubject.next(null);
  }

  updateUserProfile(id: string, user: any): Observable<any> {
    return this.http.put(`${this.API_URL}/userprofile/${id}`, user);
  }

  getUserProfile(): Observable<any> {
    return this.http.get(`${this.API_URL}/userprofile`, {});
  }

  getSelfUserProfile(): Observable<any> {
    return this.http.get(`${this.API_URL}/userprofile/getself`, {});
  }

  getUserProfileById(id: string): Observable<any> {
    return this.http.get(`${this.API_URL}/userprofile/${id}`);
  }

  deleteUserProfile(id: string): Observable<any> {
    return this.http.delete(`${this.API_URL}/userprofile/${id}`);
  }

  patchUserProfile(id: string, patchData: any): Observable<any> {
    return this.http.patch(`${this.API_URL}/userprofile/${id}`, patchData);
  }
}
