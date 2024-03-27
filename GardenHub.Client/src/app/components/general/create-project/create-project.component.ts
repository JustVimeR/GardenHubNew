import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { Router } from '@angular/router';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { RoleService } from 'src/app/services/role.service';
import StorageService from 'src/app/services/storage.service';
import { WorkTypeService } from 'src/app/services/work-type.service';


interface ServiceNode {
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

  allWorkType: any = {};

  typeOfWork: any = [];

  activeRole: 'gardener' | 'housekeeper';
  selectedFilesCount = 0;
  private _transformer = (node: ServiceNode, level: number) => {
    return {
      expandable: !!node.derivedWorkTypes && node.derivedWorkTypes.length > 0,
      name: node.label,
      level: level,
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
    private workTypeService: WorkTypeService
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
      label: item.label,
      derivedWorkTypes: item.derivedWorkTypes?.map((subItem: DerivedWorkType) => ({
        label: subItem.label
      }))
    }));
  
    this.dataSource.data = transformedData;
  }
  

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  updateTypeOfWork(name: string, isSelected: boolean) {
    if (isSelected) {
      if (!this.typeOfWork.includes(name)) {
        this.typeOfWork.push(name);
      }
    } else {
      this.typeOfWork = this.typeOfWork.filter((item: string) => item !== name);
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

 
}
