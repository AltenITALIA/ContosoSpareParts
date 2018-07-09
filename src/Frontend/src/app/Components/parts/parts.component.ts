import { Component, OnInit } from '@angular/core';
import { Part } from '../../models/part';
import { PartsService } from '../../services/parts.service'
import { MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';
@Component({
  selector: 'app-parts',
  templateUrl: './parts.component.html',
  styleUrls: ['./parts.component.css']
})
export class PartsComponent implements OnInit {

  parts: MatTableDataSource<Part>;
  part: Part = new Part();
  constructor(private partsService: PartsService) { }

  ngOnInit() {
    this.getParts();
  }
  getParts(): void {
    this.partsService.getParts()
      .subscribe(parts => this.parts = new MatTableDataSource(parts));
  }
  addPart(): void {
    this.partsService.addPart(this.part)
      .subscribe(array => {
        let data = this.parts.data;
        data.push(this.part);
        this.parts = new MatTableDataSource(data);
        this.part = new Part();
      }
      );
  }
}
