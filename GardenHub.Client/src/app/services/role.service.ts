import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import StorageService from './storage.service';


@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private activeRoleSource: BehaviorSubject<'gardener' | 'housekeeper'>;
  
  constructor(private storageService: StorageService) {
    const savedRole = this.storageService.getStringStorage('activeRole');
    const isValidRole = savedRole === 'gardener' || savedRole === 'housekeeper';
    const initialRole = isValidRole ? savedRole as 'gardener' | 'housekeeper' : 'housekeeper';
    this.activeRoleSource = new BehaviorSubject<'gardener' | 'housekeeper'>(initialRole);
  }
  
  get activeRole() {
    return this.activeRoleSource.asObservable();
  }

  setActiveRole(role: 'gardener' | 'housekeeper') {
    this.activeRoleSource.next(role);
    this.storageService.setStringStorage('activeRole', role);
  }
}
