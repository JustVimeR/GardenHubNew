import {FlatTreeControl} from '@angular/cdk/tree';
import {Component, OnInit} from '@angular/core';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { Router } from '@angular/router';
import { BreakpointObserver, BreakpointState, Breakpoints } from '@angular/cdk/layout';
import { ProjectService } from 'src/app/services/project.service';
import { WorkTypeService } from 'src/app/services/work-type.service';
import StorageService from 'src/app/services/storage.service';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { UserProfileService } from 'src/app/services/user-profile.service';

interface ServiceNode {
  id?: number;
  label: string;
  derivedWorkTypes?: ServiceNode[];
}

const TREE_DATA: ServiceNode[] = [];

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

interface DerivedWorkType {
  id: number;
  parentWorkTypeId: number;
  label: string;
}

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-service-type',
  templateUrl: './service-type.component.html',
  styleUrls: ['./service-type.component.scss']
})
export class ServiceTypeComponent extends StorageService implements OnInit{
  allWorkType: any = {};
  isPhonePortrait = false;
  isBigScreen = false;
  typeOfWork: any = []
  selfUserProfile: any = {};

  projectTitle: string = '';
  projectLocation: string = '';
  projectBudget: number = 0;
  projectDescription: string = '';
  requiredGardeners: number = 1;
  projectStatus: number = 0; 
  selectedWorkTypes: number[] = [];

  isProfileUpdatedSuccessfully = false;

  private _transformer = (node: ServiceNode, level: number) => {
    return {
      expandable: !!node.derivedWorkTypes && node.derivedWorkTypes.length > 0,
      name: node.label,
      level: level,
      id: node.id,
    };
  };

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable,
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.derivedWorkTypes,
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(
    private router: Router,
    private responsive: BreakpointObserver,
    private workTypeService: WorkTypeService,
    private projectService: ProjectService,
    private userProfileService: UserProfileService
  ) {
    super();
    this.dataSource.data = TREE_DATA;
  }

  ngOnInit() {
      this.responsive.observe(Breakpoints.HandsetPortrait)
        .subscribe(result => {

          this.isPhonePortrait = false; 
          this.isBigScreen = false;

          if (result.matches) {
            console.log("screens matches HandsetPortrait");
            this.isPhonePortrait = true;
          }
          else{
            this.isBigScreen = true;
          }

    });

    this.getWorkTypes();
    this.getUserProfile();
  } 

  updateProfile(): void {

    const updateUser = {
      name:  this.selfUserProfile.data.name,
      surname: this.selfUserProfile.data.surname,
      email: this.selfUserProfile.data.email,
      userName: this.selfUserProfile.data.userName,
      phoneNumber: this.selfUserProfile.data.phoneNumber,
      birthDate: this.selfUserProfile.data.birthDate,
      description: this.selfUserProfile.data.description,
      cities: this.selfUserProfile.data.cities,
      isGardener: true,
      icon: this.selfUserProfile.data.icon,
      descriptionOfExperience: this.selfUserProfile.data.descriptionOfExperience,

      workTypes:this.selectedWorkTypes.map(id => ({ id })) || this.selfUserProfile.data.workTypes,
    };
  
    this.userProfileService.updateUserProfile(this.selfUserProfile.data.id, updateUser).subscribe({
      next: (response) => {
        console.log('Профіль успішно оновлено', response);
        this.selfUserProfile = response;
        this.setDataStorage(StorageKey.userProfile, this.selfUserProfile);
        this.isProfileUpdatedSuccessfully = true;
      },
      error: (error) => {
        console.error('Помилка', error);
        this.isProfileUpdatedSuccessfully = false;
      }
    });
  }

  getUserProfile(): void {
    if (this.hasKeyInStorage(StorageKey.userProfile)) {
      this.selfUserProfile = this.getDataStorage(StorageKey.userProfile);
    } else {
      this.userProfileService.getSelfUserProfile().subscribe(response => {
        this.selfUserProfile = response;
        this.setDataStorage(StorageKey.userProfile, this.selfUserProfile);
      });
    }
  }


  getWorkTypes(): void {
    if (this.hasKeyInStorage(StorageKey.workType)) {
      this.allWorkType = this.getDataStorage(StorageKey.workType);
      this.transformAndSetData(this.allWorkType.data);
    } else {
      this.workTypeService.getWorkType().subscribe(response => {
        this.allWorkType = response;
        this.setDataStorage(StorageKey.workType, this.allWorkType);
        this.transformAndSetData(this.allWorkType.data);
      });
    }
  }

  transformAndSetData(apiData: any[]): void {
    const transformedData = apiData.map(item => ({
      id: item.id,
      label: item.label,
      derivedWorkTypes: item.derivedWorkTypes?.map((subItem: DerivedWorkType) => ({
        id: subItem.id,
        label: subItem.label
      }))
    }));
  
    this.dataSource.data = transformedData;
  }

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  updateTypeOfWork(name: string, isSelected: boolean, id: number) {
    if (isSelected) {
      if (!this.typeOfWork.includes(name)) {
        this.typeOfWork.push(name);
        this.selectedWorkTypes.push(id);
        console.log(name, isSelected, id);
      }
    } else {
      this.typeOfWork = this.typeOfWork.filter((item: string) => item !== name);
      this.selectedWorkTypes = this.selectedWorkTypes.filter(item => item !== id);
    }
  }

  goToProfile(){
    this.router.navigate(['/api/profile']);
  }
  
}
