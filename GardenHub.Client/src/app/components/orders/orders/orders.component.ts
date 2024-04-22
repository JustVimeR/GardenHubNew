import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import StorageService from 'src/app/services/storage.service';
import { Page } from '../models/page';
import { RoleService } from 'src/app/services/role.service';
import { ProjectService } from 'src/app/services/project.service';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { UserProfileService } from 'src/app/services/user-profile.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent extends StorageService implements OnInit{

  allProjects: any = {};
  activeRole: 'gardener' | 'housekeeper';

  constructor(
    private router: Router, 
    private roleService: RoleService,
    private projectService: ProjectService,
    private userProfileService: UserProfileService) {
    super();
    this.activeRole = 'gardener';
  }

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });

    this.getProjects();
  }

  protected readonly Page = Page;
  activePage = this.Page.inWork;

  changePage(page: Page): void {
    this.activePage = page;
  }

  getProjects(): void {
    this.userProfileService.getSelfUserProfile().subscribe(response => {
      this.allProjects = response;
      this.setDataStorage(StorageKey.userProfile, this.allProjects);
    });
  }
  

  getActiveProjects() {
    if (!this.allProjects.data) {
      return [];
    }
    return this.allProjects?.data.filter((order: any) => order?.status === 'Active');
  }
}
