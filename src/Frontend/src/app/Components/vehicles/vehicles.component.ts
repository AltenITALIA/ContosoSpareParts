import { Component, OnInit, Input } from '@angular/core';
import { VehiclesService } from '../../services/vehicles.service';
import { vehicle } from '../../models/vehicle'
import { MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';
import { VehiclesEditorComponent } from '../../components/vehicles-editor/vehicles-editor.component';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})

export class VehiclesComponent implements OnInit {

  vehicles: MatTableDataSource<vehicle>;
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

  addVehicle() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.height = '550px';
    dialogConfig.width = '650px';

    const dialogResult = this.matDialod.open(VehiclesEditorComponent, dialogConfig)
    dialogResult.afterClosed().subscribe(
      data => {
        console.log("Dialog output:", data);
        this.vehiclesService.addVehicle(data as vehicle).subscribe(
          id =>{
            var d = this.vehicles.data;
        
            console.log("vehicle created with id:", id);
          }
        );
      }
    );
  }
}


