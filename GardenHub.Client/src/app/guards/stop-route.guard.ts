import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import { MainPageService } from 'src/app/services/main-page.service';

@Injectable({
  providedIn: 'root'
})
export class StopRouteGuard implements CanActivate {
  constructor(
    private mainPageService: MainPageService
  ) {
  }

  canActivate(route: ActivatedRouteSnapshot,
              state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this.mainPageService.canRoute;
  }
}
