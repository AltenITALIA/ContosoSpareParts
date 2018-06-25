import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material'

@Component({
  selector: 'app-vehicle-delete-dialog',
  templateUrl: './vehicle-delete-dialog.component.html',
  styleUrls: ['./vehicle-delete-dialog.component.css']
})
export class VehicleDeleteDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialogRef<VehicleDeleteDialogComponent>) { }

  ngOnInit() {
  }
 
  delete(){
   this.dialogRef.close(true);
  }
}
