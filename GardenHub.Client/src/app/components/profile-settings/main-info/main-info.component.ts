import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable, map, startWith } from 'rxjs';
import StorageService from 'src/app/services/storage.service';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { RoleService } from 'src/app/services/role.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { UserProfileService } from 'src/app/services/user-profile.service';
import { StorageKey } from 'src/app/models/enums/storage-key';

@Component({
  selector: 'app-main-info',
  templateUrl: './main-info.component.html',
  styleUrls: ['./main-info.component.scss'],
})
export class MainInfoComponent extends StorageService implements OnInit{
  activeRole: 'gardener' | 'housekeeper';
  selfUserProfile: any = {};
  separatorKeysCodes: number[] = [ENTER, COMMA];
  cityCtrl = new FormControl('');
  filteredCities: Observable<string[]> | undefined;
  cities: string[] = [];
  allCities: string[] = ['Львів', 'Харків', 'Житомир', 'Івано-Франківськ', 'Рівне', 'Київ', 'Одеса', 'Кривий ріг', 'Полтава', 'Вінниця', 'Луцьк', 'Тернопіль'];
  isPhonePortrait = false;
  isBigScreen = false;

  @ViewChild('fruitInput') cityInput: ElementRef<HTMLInputElement> | undefined;

  announcer = inject(LiveAnnouncer);
  isProfileUpdatedSuccessfully = false;
  
  user = this.fb.group({
    name: [''],
    surname:[''],
    userName:[''],
    email: [''],
    phoneNumber: [''],
    birthDate: [''],
    descriptionOfExperience: new FormControl('')
  });

  constructor(
    private fb: FormBuilder,
    private roleService: RoleService, 
    private responsive: BreakpointObserver,
    private userProfileService: UserProfileService,
    ){
    super();
    this.activeRole = 'housekeeper';
    this.filteredCities = this.cityCtrl.valueChanges.pipe(
      startWith(null),
      map((city: string | null) => (city ? this._filter(city) : this.allCities.slice())),
    );
    
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
    this.roleService.activeRole.subscribe(role => {
      this.activeRole = role;
    });

    this.getUserProfile();
  }

  formatDate(date: Date | string): string {
    if (typeof date === 'string') return date;
    return new Date(date).toISOString().split('T')[0];
  }

  updateProfile(): void {
    const formValue = this.user.value;
  
    const citiesForApi = this.cities.map(cityName => ({ name: cityName }));
    
    const isGardener = this.activeRole === 'gardener';

    const updateUser = {
      name: formValue.name || this.selfUserProfile.data.name,
      surname: formValue.surname || this.selfUserProfile.data.surname,
      email: formValue.email || this.selfUserProfile.data.email,
      userName: formValue.userName || this.selfUserProfile.data.userName,
      phoneNumber: formValue.phoneNumber || this.selfUserProfile.data.phoneNumber,
      birthDate: formValue.birthDate ? this.formatDate(formValue.birthDate) : this.selfUserProfile.data.birthDate,
      description: formValue.descriptionOfExperience || this.selfUserProfile.data.description,
      cities: citiesForApi.length > 0 ? citiesForApi : this.selfUserProfile.data.cities,
      isGardener: isGardener,
      workTypes: this.selfUserProfile.data.workTypes,
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

  userProfile({value, valid}: { value: any, valid: boolean }) {
    if (valid) {
      this.updateProfile();
  } else {
      console.log("Форма має невалідні дані.");
  }
    
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
