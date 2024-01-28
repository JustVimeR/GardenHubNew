import { Component } from '@angular/core';
import {Path} from "../../models/enums/path";

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.scss']
})
export class ErrorPageComponent {
  protected readonly Path = Path;
}