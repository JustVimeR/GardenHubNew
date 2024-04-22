import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-orders-in-work',
  templateUrl: './orders-in-work.component.html',
  styleUrls: ['./orders-in-work.component.scss']
})
export class OrdersInWorkComponent implements OnInit{
  @Input() allProjects: any;
  activeRole: 'gardener' | 'housekeeper';

  constructor(
    private router: Router,
    private roleService: RoleService,
  ){}

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });
  }
  
  
  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  viewOrderDetails(orderId: number) {
    this.router.navigateByUrl(`api/orders/order/${orderId}`);
  }

  getProggressProjects() {
    if (this.activeRole == 'gardener') {
      return this.getGardenerProggressProjects();
    } else if (this.activeRole == 'housekeeper') {
      return this.getCustomerProggressProjects();
    }
    return []; 
  }

  getCustomerProggressProjects() {
    if (!this.allProjects?.data?.customerProjects) {
      return [];
    }
    return this.allProjects?.data?.customerProjects.filter((order: any) => order.status === 'InProggress');
  }

  getGardenerProggressProjects() {
    if (!this.allProjects?.data?.gardenerProjects) {
      return [];
    }
    return this.allProjects?.data?.gardenerProjects.filter((order: any) => order.status === 'InProggress');
  }
}
