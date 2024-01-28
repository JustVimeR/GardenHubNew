import { Injectable } from '@angular/core';
import {BehaviorSubject, Subject} from "rxjs";
import {SortIconComponent} from "../components/general/sort-icon/sort-icon.component";

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private showModalSubject: Subject<boolean> = new Subject<boolean>();
  showModal = this.showModalSubject.asObservable();
  dynamicComponent: BehaviorSubject<any> = new BehaviorSubject<any>(SortIconComponent);
  isModal = false;
  constructor() { }
  show(): void {
    this.showModalSubject.next(true);
    this.isModal = true;
  }

  hide(): void {
    this.showModalSubject.next(false);
    this.isModal = false;
  }
}
