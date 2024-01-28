import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable, map, startWith } from 'rxjs';
import StorageService from 'src/app/services/storage.service';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-main-info',
  templateUrl: './main-info.component.html',
  styleUrls: ['./main-info.component.scss'],
})
export class MainInfoComponent extends StorageService implements OnInit{
  activeRole: 'gardener' | 'housekeeper';
  separatorKeysCodes: number[] = [ENTER, COMMA];
  cityCtrl = new FormControl('');
  filteredCities: Observable<string[]> | undefined;
  cities: string[] = ['Київ'];
  allCities: string[] = ['Львів', 'Харків', 'Житомир', 'Івано-Франківськ', 'Рівне'];

  @ViewChild('fruitInput') cityInput: ElementRef<HTMLInputElement> | undefined;

  announcer = inject(LiveAnnouncer);

  user = this.fb.group({
    firstName: ['', Validators.required],
    lastName:['', Validators.required],
    userName:['', [Validators.required]],
    email: ['', [Validators.required]],
    phone: ['+380', [Validators.required, Validators.minLength(12)]],
    commentText: new FormControl('', [Validators.required, Validators.minLength(3)])
  });

  constructor(
    private fb: FormBuilder,
    private roleService: RoleService){
    super();
    this.activeRole = 'housekeeper';
    this.filteredCities = this.cityCtrl.valueChanges.pipe(
      startWith(null),
      map((city: string | null) => (city ? this._filter(city) : this.allCities.slice())),
    );
  }

  ngOnInit() {
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });
  }

  userProfile({value, valid}: { value: any, valid: boolean }) {
    console.log(valid);
    
   }

   addCity(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      this.cities.push(value);
    }
    event.chipInput!.clear();
  
    this.cityCtrl.setValue(null);
  }
  
  removeCity(city: string): void {
    const index = this.cities.indexOf(city);
  
    if (index >= 0) {
      this.cities.splice(index, 1);
  
      this.announcer.announce(`Видалено ${city}`);
    }
  }
  
  selectedCity(event: MatAutocompleteSelectedEvent): void {
    this.cities.push(event.option.viewValue);
    if (this.cityInput) {
      this.cityInput.nativeElement.value = '';
    }
    this.cityCtrl.setValue(null);
  }
  
  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
  
    return this.allCities.filter(city => city.toLowerCase().includes(filterValue));
  }
}
