import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import {SharedService} from "./shared.service";
import StorageService from "./storage.service";
import {StorageKey} from "../models/enums/storage-key";
import { City } from '../models/City';

@Injectable({
  providedIn: 'root'
})
export class CityService extends StorageService {
  API_URL = this.sharedService.API_URL;
  private citySubject: BehaviorSubject<any | null> = new BehaviorSubject<any| null>(null);
  city$ = this.citySubject.asObservable();
  canRoute = true;

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }

  setCity(city: any): void {
    this.citySubject.next(city);
  }

  resetCity(): void {
    this.citySubject.next(null);
  }

  createCity(city: City): Observable<any> {
    return this.http.post(`${this.API_URL}/city`, city);
  }

  updateCity(id: string, city: City): Observable<any> {
    return this.http.put(`${this.API_URL}/city/${id}`, city);
  }

  getCities(): Observable<any> {
    return this.http.get(`${this.API_URL}/city`, {});
  }

  getCityById(id: string): Observable<any> {
    return this.http.get(`${this.API_URL}/city/${id}`);
  }

  deleteCity(id: string): Observable<any> {
    return this.http.delete(`${this.API_URL}/city/${id}`);
  }

  patchCity(id: string, patchData: any): Observable<any> {
    return this.http.patch(`${this.API_URL}/city/${id}`, patchData);
  }
}
