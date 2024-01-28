import { Component } from '@angular/core';
import {Page} from "../models/page";
import {Router} from "@angular/router";
import StorageService from "../../../services/storage.service";
import {StorageKey} from "../../../models/enums/storage-key";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent extends StorageService {
  constructor(private router: Router) {
    super();
    if (this.hasKeyInStorage(StorageKey.authToken)) {
      this.router.navigateByUrl('api');
    }
  }
  protected readonly Page = Page;
  activePage = this.Page.login;

  changePage(page: Page): void {
    this.activePage = page;
  }
}
