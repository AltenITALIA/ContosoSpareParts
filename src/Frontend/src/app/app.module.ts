import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ContosoNavComponent } from './contoso-nav/contoso-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatBadgeModule, MatIconRegistry, MatTableModule, MatProgressBarModule } from '@angular/material';
import { AppRoutingModule } from './modules/app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { VehiclesComponent } from './components/vehicles/vehicles.component';
import { PartsComponent } from './components/parts/parts.component';
import { VeicleHistoryComponent } from './components/veicle-history/veicle-history.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    ContosoNavComponent,
    DashboardComponent,
    VehiclesComponent,
    PartsComponent,
    VeicleHistoryComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatBadgeModule,
    AppRoutingModule,
    MatTableModule,
    MatProgressBarModule,
    HttpClientModule
  ],
  providers: [MatIconRegistry],
  bootstrap: [AppComponent]
})
export class AppModule { }
