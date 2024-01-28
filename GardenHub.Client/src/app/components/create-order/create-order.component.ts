import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.scss']
})
export class CreateOrderComponent implements OnInit{

  activeRole: 'gardener' | 'housekeeper';

  constructor(
    private roleService: RoleService,
    private router: Router) {
    this.activeRole = 'gardener';
  }

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });
  }
}
