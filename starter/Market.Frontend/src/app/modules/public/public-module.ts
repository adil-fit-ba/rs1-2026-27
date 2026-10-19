import {NgModule} from '@angular/core';

import {PublicRoutingModule} from './public-routing-module';
import {PublicLayoutComponent} from './public-layout/public-layout.component';
import {SearchProductsComponent} from './search-products/search-products.component';
import {LandingComponent} from './landing/landing.component';
import {SharedModule} from '../shared/shared-module';


@NgModule({
  declarations: [
    PublicLayoutComponent,
    LandingComponent,
    SearchProductsComponent
  ],
  imports: [
    SharedModule,
    PublicRoutingModule,
  ]
})
export class PublicModule { }
