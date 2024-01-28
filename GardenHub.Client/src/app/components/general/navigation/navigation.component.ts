import {Component, EventEmitter, HostListener, Output} from '@angular/core';
import {AuthService} from "../../../services/auth.service";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {
  showProfileMenu = false;
  statusMenu = false;
  @Output() status: EventEmitter<boolean> = new EventEmitter<boolean>();
  @HostListener('click', ['$event'])
  signOut(event: any): void {
    event.preventDefault();
    if (event.target?.classList.contains('pr-menu-btn')) {
      this.showProfileMenu = !this.showProfileMenu;
    } else if (!event.target?.classList.contains('pr-menu-btn') && !event.target?.classList.contains('pr-menu')) {
      this.showProfileMenu = false;
    } else if (event.target?.classList.contains('logout')) {
      this.authService.resetToken();
      localStorage.clear();
    }
  }
  constructor(private authService: AuthService) {
  }

  handleMenu(): void {
    this.statusMenu = !this.statusMenu;
    this.status.emit(this.statusMenu);
  }

}
