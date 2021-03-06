import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material'
import { Vehicle } from '../../models/vehicle'
@Component({
  selector: 'app-vehicles-editor',
  templateUrl: './vehicles-editor.component.html',
  styleUrls: ['./vehicles-editor.component.css']
})
export class VehiclesEditorComponent implements OnInit {

  vehicle: Vehicle = new Vehicle();
  title = 'Create new vehicle';
  canSave = false;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialogRef<VehiclesEditorComponent>) { }

  ngOnInit() {
   
  }

  cancel(): void {
    this.dialogRef.close();
  }
  save(): void {
    this.dialogRef.close(this.vehicle);
  }
}
