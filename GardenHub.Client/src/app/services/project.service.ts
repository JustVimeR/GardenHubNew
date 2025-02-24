import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable, mergeMap, switchMap} from "rxjs";
import {SharedService} from "./shared.service";
import StorageService from "./storage.service";
import { Project } from '../models/Project';

@Injectable({
  providedIn: 'root'
})

export class ProjectService extends StorageService {
  API_URL = this.sharedService.API_URL;
  private projectSubject: BehaviorSubject<any | null> = new BehaviorSubject<any| null>(null);
  project$ = this.projectSubject.asObservable();
  canRoute = true;

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }

  setProject(project: any): void {
    this.projectSubject.next(project);
  }

  resetProject(): void {
    this.projectSubject.next(null);
  }

  createProject(project: any): Observable<any> {
    return this.http.post(`${this.API_URL}/project`, project);
  }

  getProject(): Observable<any> {
    return this.http.get(`${this.API_URL}/project`, {});
  }

  getProjectById(id: string): Observable<any> {
    return this.http.get(`${this.API_URL}/project/${id}`);
  }

  deleteProject(id: string): Observable<any> {
    return this.http.delete(`${this.API_URL}/project/${id}`);
  }

  updateProject(id: string, projectData: any): Observable<any> {
    return this.http.put(`${this.API_URL}/project/${id}`, projectData);
  }
  

}
