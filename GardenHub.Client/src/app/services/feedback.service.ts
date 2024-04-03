import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {SharedService} from "./shared.service";
import StorageService from "./storage.service";

@Injectable({
  providedIn: 'root'
})

export class FeedbackService extends StorageService {
  API_URL = this.sharedService.API_URL;
  private feedbackSubject: BehaviorSubject<any | null> = new BehaviorSubject<any| null>(null);
  feedback$ = this.feedbackSubject.asObservable();
  canRoute = true;

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }

  setFeedback(feedback: any): void {
    this.feedbackSubject.next(feedback);
  }

  resetFeedback(): void {
    this.feedbackSubject.next(null);
  }

  createFeedback(feedback: any): Observable<any> {
    return this.http.post(`${this.API_URL}/feedback`, feedback);
  }

  getFeedback(): Observable<any> {
    return this.http.get(`${this.API_URL}/feedback`, {});
  }

  getFeedbackById(id: string): Observable<any> {
    return this.http.get(`${this.API_URL}/feedback/${id}`);
  }

  deleteFeedback(id: string): Observable<any> {
    return this.http.delete(`${this.API_URL}/feedback/${id}`);
  }

}
