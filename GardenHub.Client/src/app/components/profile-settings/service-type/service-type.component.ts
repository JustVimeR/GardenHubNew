import {FlatTreeControl} from '@angular/cdk/tree';
import {Component} from '@angular/core';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { Router } from '@angular/router';

interface ServiceNode {
  name: string;
  children?: ServiceNode[];
}

const TREE_DATA: ServiceNode[] = [
  {
    name: 'Стрижка та обрізка',
    children: [
      {name: 'Обрізання дерев'}, 
      {name: 'Стрижка кущів'}, 
      {name: 'Кронування дерев'},
      {name: 'Зрізання дерев'}
    ],
  },
  {
    name: 'Догляд за газоном',
    children: [
      {name: 'Покіс газону'}, 
      {name: "Обробка газону від бур'янів"}, 
      {name: 'Підсіювання газону'},
      {name: 'Аєрація'},
      {name: 'Скарифікація'},
      {name: 'Влаштування ручного поливу'}
    ],
  },
  {
    name: 'Захист від захворювань та шкідників',
    children: [
      {name: 'Діагностика захворювань'}, 
      {name: 'Лікування захворювань'}, 
      {name: 'Профілактична обробка ділянки'},
      {name: 'Боротьба зі шкідниками'}
    ],
  },
  {
    name: 'Підготовка до зими',
    children: [
      {name: 'Укриття кущів та дерев на зиму'},
      {name: 'Підготовка газону до зими'},
      {name: 'Прибирання ділянки перед зимою'}
    ],
  },
  {
    name: 'Створення нових насаджень',
    children: [
      {name: 'Підбір рослин'},
      {name: 'Визначення зональності майбутньої посадки'},
      {name: 'Підготовка ґрунту та посадка рослин'}
    ],
  },
  {
    name: 'Планування та дизайн саду',
    children: [
      {name: 'Влаштування тематичних зон'},
      {name: 'Проектування та встановлення системи поливу'}
    ],
  },
  {
    name: 'Догляд за водними об\'єктами',
    children: [
      {name: 'Догляд за водоймами, фонтанами і тд'},
      {name: 'Обслуговування системи фільтрації та очищення води'}
    ],
  },
  {
    name: 'Ландшафтна архітектура',
    children: [
      {name: 'Проектування ландшафтних об\'єктів'},
      {name: 'Створення зон для відпочинку'}
    ],
  }
];

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
export class ServiceTypeComponent {

  typeOfWork: any = [
    'Обрізання дерев', 'Стрижка кущів', 'Кронування дерев'
  ]

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

  constructor(private router: Router) {
    this.dataSource.data = TREE_DATA;
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

  goToProfile(){
    this.router.navigate(['/api/profile']);
  }
}
