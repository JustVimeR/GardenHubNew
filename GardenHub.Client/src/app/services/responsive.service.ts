import { Injectable } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';

@Injectable({
  providedIn: 'root'
})
export class ResponsiveService {

  constructor(private breakpointObserver: BreakpointObserver) {
    
  }

  checkScreenSize(){
    return this.breakpointObserver.observe(Breakpoints.HandsetPortrait)
  }
}
