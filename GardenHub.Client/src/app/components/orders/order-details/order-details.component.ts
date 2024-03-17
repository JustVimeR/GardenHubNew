import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderStatus } from 'src/app/models/enums/order-status';
import { ProjectService } from 'src/app/services/project.service';
import StorageService from 'src/app/services/storage.service';
import { ActivatedRoute } from '@angular/router';

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

  order: any;

  savedRole = this.storageService.getStringStorage('activeRole');
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private storageService: StorageService,
    private projectService: ProjectService
    ){
    super();  
  }

  ngOnInit(){
    const orderId = this.route.snapshot.paramMap.get('id');
    if (orderId) {
      this.projectService.getProjectById(orderId).subscribe(order => {
        this.order = order;
      }, error => {
        console.error("Failed to fetch order details:", error);
      });
    }
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
