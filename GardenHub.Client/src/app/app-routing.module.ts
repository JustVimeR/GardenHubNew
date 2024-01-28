import {RouterModule, Routes} from "@angular/router";
import {AuthComponent} from "./components/auth/auth/auth.component";
import {NgModule} from "@angular/core";
import {Path} from "./models/enums/path";
import { isAuthGuard } from "./guards/is-auth.guard";


const routes: Routes = [
  { path: '', redirectTo: Path.auth, pathMatch: 'full' },
  { path: Path.auth, component: AuthComponent },
  {
    path: 'api',
    loadChildren: () => import('./components/main/main.module').then(m => m.MainModule),
    canActivate: [isAuthGuard]
  },
  { path: '**', redirectTo: Path.auth }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
