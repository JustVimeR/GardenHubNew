import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { OrderStatus } from 'src/app/models/enums/order-status';
import { ProjectService } from 'src/app/services/project.service';
import StorageService from 'src/app/services/storage.service';
import { ActivatedRoute } from '@angular/router';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { SignalRService } from 'src/app/services/signalR.service';
import { FeedbackService } from 'src/app/services/feedback.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent extends StorageService implements OnInit{
  color: string = '';
  showSuccessfulOrder = false;
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
    private location: Location,
    private signalRService: SignalRService,
    private feedbackService: FeedbackService
    ){
    super();  
  }

  ngOnInit(){
    this.getOrderId();
  }

  applyToProject() {
    if (this.order && this.order.data) {
      const message = this.order.data.title;
      const projectId = this.order.data.id;
  
      this.signalRService.sendProjectApplyNotification(message, projectId.toString());
      console.log("Запит на проект відправлено:", message, "до", projectId);
    } else {
      console.error("Інформація про проект недоступна");
    }
  }
  

  getOrderId(){
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

  isMyProject(){
    const userProfileObject = this.storageService.getDataStorage(StorageKey.userProfile);
    if(this.order?.data?.customerId == userProfileObject?.data?.id){
      return true;
    }else{
      return false;
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
    if (this.order?.data?.gardeners?.length > 0) {
      const gardenerUserName = this.order.data.gardeners[0].id;
      this.router.navigateByUrl(`/api/homeowner-profile/${gardenerUserName}`);
    } else {
      console.error('No gardeners found or invalid order structure');
    }
  }

  onComplete(): void {
    const projectId = this.order?.data?.id;
    if (!projectId) return;

    this.projectService.getProjectById(projectId).subscribe(project => {
      const updatedProject = {
        title: project.data.title,
        location: project.data.location,
        budget: project.data.budget,
        description: project.data.description,
        numberOfRequriedGardeners: project.data.numberOfRequriedGardeners,
        status: 2,
        startDate: project.data.startDate,
        endDate: project.data.endDate,
        workTypes: project.data.workTypes
      };

      this.projectService.updateProject(projectId, updatedProject).subscribe(
        response => {
          console.log('Project updated successfully:', response);
            this.showSuccessfulOrder = true;
        },
        error => {
          console.error('Error updating project:', error);
        }
      );
    });
  }

  handleFeedback(feedback: any){
    if (this.order && this.order.data) {
      const fullFeedback = {
        rating: feedback.rating,
        text: feedback.text,
        gardenerId: this.order.data.gardeners?.id,
        projectId: this.order.data.id
      };
      this.feedbackService.createFeedback(fullFeedback).subscribe({
        next: (response) => {
          console.log('Feedback submitted successfully', response);
          this.showSuccessfulOrder = false;
        },
        error: (error) => {
          console.error('Error submitting feedback', error);
          this.showSuccessfulOrder = false;
        }
      });
    } else {
      console.error('Order details are missing, cannot submit feedback');
      this.showSuccessfulOrder = false;
    }
  }

}
