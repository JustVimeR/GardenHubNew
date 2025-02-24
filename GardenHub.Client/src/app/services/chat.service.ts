import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {SharedService} from "./shared.service";
import StorageService from "./storage.service";

@Injectable({
  providedIn: 'root'
})

export class ChatService extends StorageService {
  API_URL = this.sharedService.API_URL;
  private chatSubject: BehaviorSubject<any | null> = new BehaviorSubject<any| null>(null);
  chat$ = this.chatSubject.asObservable();
  canRoute = true;

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }

  getChats(): Observable<any> {
    return this.http.get(`${this.API_URL}/message/chats`, {});
  }

  getChatById(chatId: string): Observable<any> {
    return this.http.get(`${this.API_URL}/message/chats/${chatId}`);
  }

  getNotifications(): Observable<any> {
    return this.http.get(`${this.API_URL}/message/notifications`, {});
  }

  getProjectAccept(params: HttpParams): Observable<any> {
    return this.http.get(`${this.API_URL}/project/acceptapply`, { params });
  }

  deleteNotification(id: string): Observable<any>{
    return this.http.delete(`${this.API_URL}/message/notifications/${id}`);
  }


}
