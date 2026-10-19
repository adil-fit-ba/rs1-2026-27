import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientHomeComponent } from './client-home/client-home.component';

// Rute ispod /client (guard u app-routing-module.ts: bilo koji prijavljeni korisnik)
const routes: Routes = [
  // default → /client
  { path: '', component: ClientHomeComponent, pathMatch: 'full' },
  // kasnije npr.: { path: 'orders', component: ClientOrdersComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
