import { Component, OnInit, Input } from '@angular/core';
import { VehiclesService } from '../../services/vehicles.service';
import { Vehicle } from '../../models/vehicle'
import { MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';
import { VehiclesEditorComponent } from '../../components/vehicles-editor/vehicles-editor.component';
import { SelectionModel } from '@angular/cdk/collections';
import { VehicleDeleteDialogComponent } from '../../components/vehicle-delete-dialog/vehicle-delete-dialog.component'
import { AppInsightsService } from '@markpieszak/ng-application-insights';
@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})

export class VehiclesComponent implements OnInit {

  vehicles: MatTableDataSource<Vehicle>;
  selection = new SelectionModel<Vehicle>(true, []);
  selectionMode = false;
  constructor(private vehiclesService: VehiclesService, private matDialod: MatDialog, private appInsightsService: AppInsightsService) { }

  ngOnInit() {
    this.getVehicles();
  }

  getVehicles(): void {
    this.vehiclesService.getVehicles()
      .subscribe(vehicle => {
        this.appInsightsService.trackEvent("fetch vehicles services");
        this.vehicles = new MatTableDataSource(vehicle)
      });
  }

  applyFilter(filterValue: string): void {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.appInsightsService.trackEvent("search for", { key:filterValue});
    this.vehicles.filter = filterValue;
  }
  toggleSelection() {
    this.selection.clear();
    this.selectionMode = !this.selectionMode;
  }
  addVehicle() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.height = '550px';
    dialogConfig.width = '650px';

    const dialogResult = this.matDialod.open(VehiclesEditorComponent, dialogConfig);
    dialogResult.afterClosed().subscribe(
      data => {
        console.log("Dialog output:", data);
        if (data != undefined) {
          this.vehiclesService.addVehicle(data as Vehicle).subscribe(
            vehicleId => {
              let d = this.vehicles.data;
              d.push(data as Vehicle);
              this.vehicles = new MatTableDataSource(d);
              this.appInsightsService.trackEvent("create vehicle", {key:JSON.stringify(data)});
              console.log("vehicle created with id:", vehicleId);
            }
          );
        }
      }
    );
  }
  deleteVehicle(vehicle: Vehicle) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.height = '350px';
    dialogConfig.width = '550px';
    dialogConfig.data = vehicle;
    const dialogResult = this.matDialod.open(VehicleDeleteDialogComponent, dialogConfig);

    dialogResult.afterClosed().subscribe(
      data => {
        console.log("Dialog output:", data);
        if (data) {
          this.vehiclesService.deleteVehicle(vehicle).subscribe(
            v => {
              console.log(v);
              let d = this.vehicles.data;
              let index: number = d.indexOf(vehicle);
              if (index !== -1) {
                  d.splice(index, 1);
              }
              this.vehicles = new MatTableDataSource(d);
            }
          );
        }
      }
    );
  }
}


