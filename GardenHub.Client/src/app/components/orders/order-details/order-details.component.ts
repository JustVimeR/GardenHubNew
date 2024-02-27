import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderStatus } from 'src/app/models/enums/order-status';
import StorageService from 'src/app/services/storage.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent extends StorageService implements OnInit{
  color: string = '';
  fakeData = {
    status: OrderStatus.complited
  }

  savedRole = this.storageService.getStringStorage('activeRole');
  constructor(
    private router: Router,
    private storageService: StorageService
    ){
    super();  
  }
  ngOnInit(){
    console.log(this.savedRole)
  }

  back() {
    this.router.navigate(['/api/orders']);
  }

  viewAuthor() {
    this.router.navigateByUrl(`api/homeowner-profile`);
  }
  viewPerformer() {
    this.router.navigateByUrl(`api/orders/gardener-profile`);
  }


}
