import { A } from '@angular/cdk/keycodes';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { Router } from '@angular/router';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { ProjectService } from 'src/app/services/project.service';
import { RoleService } from 'src/app/services/role.service';
import StorageService from 'src/app/services/storage.service';
import { WorkTypeService } from 'src/app/services/work-type.service';


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

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.scss']
})
export class CreateProjectComponent extends StorageService implements OnInit{

  projectTitle: string = '';
  projectLocation: string = '';
  projectBudget: number = 0;
  projectDescription: string = '';
  requiredGardeners: number = 1;
  projectStatus: number = 0; 
  selectedWorkTypes: number[] = [];


  allWorkType: any = {};
  typeOfWork: any = [];
  isProjectCreatedSuccessfully = false;

  activeRole: 'gardener' | 'housekeeper';
  selectedFilesCount = 0;
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
    private roleService: RoleService,
    private workTypeService: WorkTypeService,
    private projectService: ProjectService
    ) {
    super();
    this.dataSource.data = TREE_DATA;
    this.activeRole = 'gardener';
  }

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });
    this.getWorkTypes();
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

  onFileSelected(event: any) {
    const files = event.target.files;
    this.selectedFilesCount = files.length;
    if (files) {
      // потом нада додать сюда обробку файла
      console.log(files);
    }
  }

  triggerInput() {
    document.getElementById('fileInput')!.click();
  }

  createProject(): void {
    const project = {
      title: this.projectTitle,
      location: this.projectLocation,
      budget: this.projectBudget,
      description: this.projectDescription,
      numberOfRequriedGardeners: this.requiredGardeners,
      status: this.projectStatus,
      workTypes: this.selectedWorkTypes.map(id => ({ id }))
    };

    this.projectService.createProject(project).subscribe({
      next: (response) => {
        console.log('Project created successfully', response);
        this.isProjectCreatedSuccessfully = true;
        this.updateProjectInLocalStorage(response.data);
      },
      error: (error) => {
        console.error('There was an error creating the project', error);
      }
    });
  }

  updateProjectInLocalStorage(newProject: any): void {
    const projectKey = StorageKey.project;
    const projectObject = this.getDataStorage(projectKey);

    if (projectObject && projectObject.data) {
        projectObject.data.push(newProject);
        this.setDataStorage(projectKey, projectObject);
    } else {
        const initialProjectData = {
            errors: [],
            data: [newProject]
        };
        this.setDataStorage(projectKey, initialProjectData);
    }
}

}
