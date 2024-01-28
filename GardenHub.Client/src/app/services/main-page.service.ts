import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject} from "rxjs";
import {SharedService} from "./shared.service";
import {StorageKey} from "../models/enums/storage-key";
import StorageService from "./storage.service";

@Injectable({
  providedIn: 'root'
})
export class MainPageService extends StorageService {
  API_URL = this.sharedService.API_URL;
  canRoute = true;

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) {
    super();
  }
}