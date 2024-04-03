import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { OrderStatus } from 'src/app/models/enums/order-status';
import { ProjectService } from 'src/app/services/project.service';
import StorageService from 'src/app/services/storage.service';
import { ActivatedRoute } from '@angular/router';
import { StorageKey } from 'src/app/models/enums/storage-key';

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
    private projectService: ProjectService,
    private location: Location
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

  deleteOrder() {
    const projectId = this.order?.data?.id;
    if (projectId) {
      this.projectService.deleteProject(projectId).subscribe({
        next: () => {
          console.log("Project successfully deleted");
          this.deleteOrderFromStorage(projectId);
          this.back();
        },
        error: (error) => {
          console.error("Failed to delete project:", error);
        }
      });
    } else {
      console.error('Project ID is missing');
    }
  }
  

  deleteOrderFromStorage(projectId: number) {
    const projectKey = StorageKey.project;
    const projectObject = this.storageService.getDataStorage(projectKey);
    
    if (projectObject && projectObject.data && projectObject.data.length) {

      projectObject.data = projectObject.data.filter((project: any) => project.id !== projectId);

      this.storageService.setDataStorage(projectKey, projectObject);
    }
  }
  
  
  

  back() {
    this.location.back();
  }


  viewAuthor() {
    const customerId = this.order?.data?.customerId;
    if (customerId) {
      this.router.navigateByUrl(`/api/homeowner-profile/${customerId}`);
    } else {
      console.error('Customer ID is missing');
    }
  }
  
  viewPerformer() {
    this.router.navigateByUrl(`api/orders/gardener-profile`);
  }


}
