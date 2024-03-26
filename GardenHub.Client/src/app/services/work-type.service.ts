import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {SharedService} from "./shared.service";
import StorageService from "./storage.service";

@Injectable({
  providedIn: 'root'
})

export class WorkTypeService extends StorageService {
  API_URL = this.sharedService.API_URL;
  private worktypeSubject: BehaviorSubject<any | null> = new BehaviorSubject<any| null>(null);
  worktype$ = this.worktypeSubject.asObservable();
  canRoute = true;

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }

  setWorkType(worktype: any): void {
    this.worktypeSubject.next(worktype);
  }

  resetWorkType(): void {
    this.worktypeSubject.next(null);
  }

  createWorkType(worktype: any): Observable<any> {
    return this.http.post(`${this.API_URL}/worktype`, worktype);
  }

  getWorkType(): Observable<any> {
    return this.http.get(`${this.API_URL}/worktype`, {});
  }

  getWorkTypeById(id: string): Observable<any> {
    return this.http.get(`${this.API_URL}/worktype/${id}`);
  }

  deleteWorkType(id: string): Observable<any> {
    return this.http.delete(`${this.API_URL}/worktype/${id}`);
  }

}
