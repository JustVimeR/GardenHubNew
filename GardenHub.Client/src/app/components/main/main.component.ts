import { Component, HostListener, OnInit} from '@angular/core';
import {ModalService} from "../../services/modal.service";
import {AuthService} from "../../services/auth.service";
import { RoleService } from 'src/app/services/role.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit{
  show$ = this.modalService.showModal;
  statusMenu = false;
  showProfileMenu = false;
  activeTab: 'gardener' | 'housekeeper' = 'housekeeper';
  activeRole: 'gardener' | 'housekeeper';
  
  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
      this.activeTab = role; 
    });
  }

  @HostListener('click', ['$event'])
  trackClick(event: any): void {
    if (event.target?.classList.contains('sidebar-nav-link')) {
      event.preventDefault();
      if (event.target?.classList.contains('active')) {
        event.target?.classList.remove('active');
      } else {
        this.removeClassforAllNav()
        event.target?.classList.add('active');
      }
    }
    if (event.target?.classList.contains('pr-menu-btn')) {
      event.preventDefault();
      this.showProfileMenu = !this.showProfileMenu;
    } else if (!event.target?.classList.contains('pr-menu-btn') && !event.target?.classList.contains('pr-menu')) {
      this.showProfileMenu = false;
    } else if (event.target?.classList.contains('logout')) {
      event.preventDefault();
      this.authService.resetToken();
      localStorage.clear();
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event.target.innerWidth < 1200) {
      this.statusMenu = true;
    } else {
      this.statusMenu = false;
    }
  }

  constructor(
    private modalService: ModalService,
    private authService: AuthService,
    private roleService: RoleService,
    private router:Router
  ) {
    this.activeRole = 'gardener';
  }

  removeClassforAllNav(): void {
    let listNavItem = document.getElementsByClassName('sidebar-nav-link');
    for (let item of Object.keys(listNavItem)) {
      // @ts-ignore
      listNavItem[item].classList.remove('active');
    }
  }

  handleMenu(): void {
    this.statusMenu = !this.statusMenu;
  }

  setActiveTab(tab: 'gardener' | 'housekeeper') {
    this.activeTab = tab;
    this.roleService.setActiveRole(tab);
    this.router.navigate(['/api/main']);
  }
}
