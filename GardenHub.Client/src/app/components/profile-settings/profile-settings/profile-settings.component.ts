import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import StorageService from 'src/app/services/storage.service';
import { Page } from '../models/page';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss']
})
export class ProfileSettingsComponent extends StorageService implements OnInit{
  activeRole: 'gardener' | 'housekeeper';
  
  constructor(
    private router: Router,
    private roleService: RoleService
    ) {
    super();
    this.activeRole = 'gardener';
  }

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });
  }

  protected readonly Page = Page;
  activePage = this.Page.mainInfo;

  changePage(page: Page): void {
    this.activePage = page;
  }
}
