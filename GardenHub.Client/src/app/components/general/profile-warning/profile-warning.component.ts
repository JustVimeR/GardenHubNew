import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile-warning',
  templateUrl: './profile-warning.component.html',
  styleUrls: ['./profile-warning.component.scss']
})
export class ProfileWarningComponent {

  constructor(private router: Router){
    
  }

  goToProfile(): void {
    this.router.navigate(['/api/profilesettings']);
  }
}
