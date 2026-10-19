import {NgModule} from '@angular/core';

import {ClientRoutingModule} from './client-routing-module';
import {SharedModule} from '../shared/shared-module';
import {ClientHomeComponent} from './client-home/client-home.component';


@NgModule({
  declarations: [
    ClientHomeComponent
  ],
  imports: [
    SharedModule,
    ClientRoutingModule
  ]
})
export class ClientModule { }
