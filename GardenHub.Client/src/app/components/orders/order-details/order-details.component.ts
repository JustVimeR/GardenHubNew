import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent {

  constructor(private router: Router){

  }

  back() {
    this.router.navigate(['/api/orders']);
  }

  viewAuthor() {
    this.router.navigateByUrl(`api/orders/gardener-profile`);
  }
}
