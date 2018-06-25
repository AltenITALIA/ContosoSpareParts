import { Component, OnInit, Input } from '@angular/core';
import { VehiclesService } from '../../services/vehicles.service';
import { vehicle } from '../../models/vehicle'
import { MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';
import { VehiclesEditorComponent } from '../../components/vehicles-editor/vehicles-editor.component';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})

export class VehiclesComponent implements OnInit {

  vehicles: MatTableDataSource<vehicle>;
  selection = new SelectionModel<vehicle>(true, []);
  selectionMode = false;
  constructor(private vehiclesService: VehiclesService, private matDialod: MatDialog) { }

  ngOnInit() {
    this.getVehicles();
  }

  getVehicles(): void {
    this.vehiclesService.getVehicles()
      .subscribe(vehicle => this.vehicles = new MatTableDataSource(vehicle));
  }

  applyFilter(filterValue: string): void {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
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

    const dialogResult = this.matDialod.open(VehiclesEditorComponent, dialogConfig)
    dialogResult.afterClosed().subscribe(
      data => {
        console.log("Dialog output:", data);
        if (data != undefined) {
          this.vehiclesService.addVehicle(data as vehicle).subscribe(
            id => {
              var d = this.vehicles.data;
              d.push(data as vehicle);
              this.vehicles = new MatTableDataSource(d);
              console.log("vehicle created with id:", id);
            }
          );
        }
      }
    );
  }
}


