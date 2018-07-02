import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ContosoNavComponent } from './contoso-nav/contoso-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatBadgeModule, MatIconRegistry, MatTableModule, MatProgressBarModule, MatInputModule, MatChipsModule, MatDialogModule, MatFormFieldModule, MatDatepickerModule, MatNativeDateModule, MatCheckboxModule, MatSnackBarModule, MatCardModule } from '@angular/material';
import { AppRoutingModule } from './modules/app-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { VehiclesComponent } from './components/vehicles/vehicles.component';
import { PartsComponent } from './components/parts/parts.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ProgressComponent } from './components/progress/progress.component';
import { ContosoHttpInterceptor } from '../app/services/contoso-http-interceptor';
import { VehiclesEditorComponent } from './components/vehicles-editor/vehicles-editor.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { VehicleDeleteDialogComponent } from './components/vehicle-delete-dialog/vehicle-delete-dialog.component';
import { ColorPickerModule } from 'ngx-color-picker';
import {safeColor} from '../app/pipes/safeColor';
import { VehicleHistoryComponent } from './components/vehicle-history/vehicle-history.component';
import { ApplicationInsightsModule, AppInsightsService } from '@markpieszak/ng-application-insights';
@NgModule({
  declarations: [
    AppComponent,
    ContosoNavComponent,
    DashboardComponent,
    VehiclesComponent,
    PartsComponent,
    ProgressComponent,
    VehiclesEditorComponent,
    VehicleDeleteDialogComponent,
    safeColor,
    VehicleHistoryComponent,
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
    HttpClientModule,
    MatInputModule,
    MatChipsModule,
    MatDialogModule,
    MatFormFieldModule,
    FormsModule,
    MatDatepickerModule, 
    MatNativeDateModule,
    MatCheckboxModule,
    MatSnackBarModule,
    ColorPickerModule,
    MatCardModule,
    ApplicationInsightsModule.forRoot({
      instrumentationKey: '526666ce-fbc3-42d4-af7a-a63fedc30908'})
  ],
  providers: [AppInsightsService,MatIconRegistry,
    {
      provide: HTTP_INTERCEPTORS,
      useExisting: ContosoHttpInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent],
  entryComponents: [VehiclesEditorComponent, VehicleDeleteDialogComponent]
})
export class AppModule { }
