import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from '../components/dashboard/dashboard.component';
import { VehiclesComponent } from '../components/vehicles/vehicles.component';
import { PartsComponent } from '../components/parts/parts.component';
import { VeicleHistoryComponent } from '../components/veicle-history/veicle-history.component';


const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'vehicles', component: VehiclesComponent },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'parts', component: PartsComponent },
  { path: 'history', component: VeicleHistoryComponent }
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})

export class AppRoutingModule {
}


