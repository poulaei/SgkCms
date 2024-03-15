import { NgModule } from '@angular/core';
import { LedgergroupComponent } from './ledgergroup.component';
import { LedgergroupRoutingModule } from './ledgergroup-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [LedgergroupComponent],
  imports: [
    LedgergroupRoutingModule,
    SharedModule,
  ]
})
export class LedgergroupModule { }
