import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LedgergroupComponent } from './ledgergroup.component';
import { AuthGuard, PermissionGuard } from '@abp/ng.core';


const routes: Routes = [
  { path: '', component: LedgergroupComponent, canActivate: [AuthGuard, PermissionGuard] },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LedgergroupRoutingModule { }
