import { Component, Input, OnInit } from '@angular/core';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-orders-complited',
  templateUrl: './orders-complited.component.html',
  styleUrls: ['./orders-complited.component.scss']
})

export class OrdersComplitedComponent implements OnInit{
  @Input() allProjects: any;
  activeRole: 'gardener' | 'housekeeper';
  
  constructor(
    private roleService: RoleService
  ){}

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });
  }

  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  getCompletedProjects() {
    if (this.activeRole == 'gardener') {
      return this.getGardenerCompletedProjects();
    } else if (this.activeRole == 'housekeeper') {
      return this.getCustomerCompletedProjects();
    }
    return []; 
  }

  getCustomerCompletedProjects() {
    if (!this.allProjects?.data?.customerProjects) {
      return [];
    }
    return this.allProjects?.data?.customerProjects.filter((order: any) => order.status === 'Completed');
  }

  getGardenerCompletedProjects() {
    if (!this.allProjects?.data?.gardenerProjects) {
      return [];
    }
    return this.allProjects?.data?.gardenerProjects.filter((order: any) => order.status === 'Completed');
  }

}
