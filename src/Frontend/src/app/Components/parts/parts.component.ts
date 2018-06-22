import { Component, OnInit } from '@angular/core';
import { part } from '../../models/part';
import { PartsService } from '../../services/parts.service'

@Component({
  selector: 'app-parts',
  templateUrl: './parts.component.html',
  styleUrls: ['./parts.component.css']
})
export class PartsComponent implements OnInit {

  parts: part[];
  constructor(private partsService: PartsService) { }

  ngOnInit() {
    this.getParts();
  }
  getParts(): void {
    this.partsService.getParts()
      .subscribe(parts => this.parts = parts);
  }
}
