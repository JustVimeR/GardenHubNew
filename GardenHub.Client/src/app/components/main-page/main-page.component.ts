import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderStatus } from 'src/app/models/enums/order-status';
import { RoleService } from 'src/app/services/role.service';
import { MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Order } from 'src/app/models/order';
import { CityService } from 'src/app/services/city.service';
import StorageService from 'src/app/services/storage.service';
import { SharedService } from 'src/app/services/shared.service';
import { StorageKey } from 'src/app/models/enums/storage-key';
import { City } from 'src/app/models/City';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { SuccessfullOrderComponent } from '../general/successfull-order/successfull-order.component';
import { ProjectService } from 'src/app/services/project.service';

interface ServiceNode {
  name: string;
  selected?: boolean;
  children?: ServiceNode[];
}

const TREE_DATA: ServiceNode[] = [
  {
    name: 'За категорією',
    children: [
      {name: 'Стрижка та обрізка', selected: false}, 
      {name: 'Догляд за газоном', selected: false}, 
      {name: 'Захист від захворювань та шкідників', selected: false},
      {name: 'Підготовка до зими', selected: false},
      {name: 'Створення нових насаджень', selected: false}, 
      {name: 'Планування та дизайн саду', selected: false}, 
      {name: 'Догляд за водними об’єктами', selected: false},
      {name: 'Ландшафтна архітектура', selected: false}
    ],
  },
  {
    name: 'За локацією',
    children: [
      {name: 'Київ', selected: false}, 
      {name: "Харків", selected: false}, 
      {name: 'Львів', selected: false},
      {name: 'Житомир', selected: false}
    ],
  }
];

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent extends StorageService implements OnInit{
  citys: any[] = [];
  cities: any[] = [];
  selectedCity: City | null = null;
  isFilterOpen: boolean = false;
  currentPageIndex = 0;
  ordersPerPage = 4;
  visibleOrders: any = [];
  allProjects: any = {};

  activeRole: 'gardener' | 'housekeeper';

  private _transformer = (node: ServiceNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
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
    node => node.children,
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
  constructor(
    private sharedService: SharedService,
    private roleService: RoleService,
    private router: Router,
    private cityService: CityService,
    private projectService: ProjectService
    ) {
    super();
    this.activeRole = 'gardener';
    this.dataSource.data = TREE_DATA;
  }

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });
    this.getCities();
    this.getProjects();
  }

  getCities(): void {
    if (this.hasKeyInStorage(StorageKey.city)) {
      this.cities = this.getDataStorage(StorageKey.city);
    } else {
      this.cityService.getCities().subscribe(response => {
        this.cities = response;
        this.setDataStorage(StorageKey.city, this.cities);
      });
    }
  }


  getProjects(): void {
    if (this.hasKeyInStorage(StorageKey.project)) {
      this.allProjects = this.getDataStorage(StorageKey.project);
    } else {
      this.projectService.getProject().subscribe(response => {
        this.allProjects = response;
        this.setDataStorage(StorageKey.project, this.allProjects);
      });
    }
  }

  getActiveProjects() {
    if (!this.allProjects.data) {
      return [];
    }
    return this.allProjects.data.filter((order: any) => order.status === 'Active');
  }
  

  toggleFilter() {
    this.isFilterOpen = !this.isFilterOpen;
  }

  goToCreateOrder(){
    this.router.navigate(['/api/create-order']);
  }

  toggleHeart(order: any) {
    order.isHeartClicked = !order.isHeartClicked;
  }

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  fakeData: any = [
    {
      workerPhoto: "../../../assets/photo1.png",
      username: "juli.gardener",
      raiting: '5.0'
    },
    {
      workerPhoto: "../../../assets/photo2.png",
      username: "anna.wodj",
      raiting: '5.0'
    },
    {
      workerPhoto: "../../../assets/photo3.png",
      username: "anton__one",
      raiting: '4.9'
    },
    {
      workerPhoto: "../../../assets/photo4.png",
      username: "vlad.taran",
      raiting: '4.8'
    },
    {
      workerPhoto: "../../../assets/photo5.png",
      username: "emiliartp",
      raiting: '4.8'
    },
    {
      workerPhoto: "../../../assets/photo6.png",
      username: "_sergart_",
      raiting: '4.7'
    },
    {
      workerPhoto: "../../../assets/photo7.png",
      username: "oleg_vnek",
      raiting: '4.6'
    },
  ];

  
  // getFilteredOrders() {
  //   const selectedCategories = this.getSelectedCategories();
  //   const selectedLocations = this.getSelectedLocations();
  
  //   return this.fakeOrders.filter((order: Order) => {
  //     const matchesCategory = selectedCategories.length === 0 || order.typeOfWork.some((work: string) => selectedCategories.includes(work));
  //     const matchesLocation = selectedLocations.length === 0 || selectedLocations.includes(order.location);
  //     return matchesCategory && matchesLocation;
  //   });
  // }

  // getSelectedLocations(): string[] {
  //   const selectedLocations: string[] = [];
  //   const locationNode = TREE_DATA.find(node => node.name === 'За локацією');
  //   if (locationNode && locationNode.children) {
  //     locationNode.children.forEach(child => {
  //       if (child.selected) {
  //         selectedLocations.push(child.name);
  //       }
  //     });
  //   }
  //   return selectedLocations;
  // }

  // getSelectedCategories(): string[] {
  //   const selectedCategories: string[] = [];
  //   TREE_DATA.forEach(node => {
  //     if (node.children) {
  //       node.children.forEach(child => {
  //         if (child.selected) {
  //           selectedCategories.push(child.name);
  //         }
  //       });
  //     }
  //   });
  //   return selectedCategories;
  // }

}

