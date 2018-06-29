import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { VehiclesService } from '../../services/vehicles.service';
import { VehicleHistoryService } from '../../services/vehicle-history.service';
import { HistoryItem } from '../../models/historyItem';
import { Vehicle } from '../../models/vehicle';

@Component({
  selector: 'app-vehicle-history',
  templateUrl: './vehicle-history.component.html',
  styleUrls: ['./vehicle-history.component.css']
})
export class VehicleHistoryComponent implements OnInit {

  
vehicleHistoryItems : HistoryItem[];
  constructor(private location: Location,
    private route: ActivatedRoute,
    private vehicleService: VehiclesService,
    private vehicleHistoryService: VehicleHistoryService) { }


  ngOnInit() {
    this.getVehicleHistroy();
  }

  getVehicleHistroy(): void {
    const plate = this.route.snapshot.paramMap.get('id');
    this.vehicleService.getVehiclesByPlate(plate)
    .subscribe(
      vehicles => this.vehicleHistoryService.getHistoryByVehicleId(vehicles[0].id)
      .subscribe(
        historyItems =>{ 

          this.vehicleHistoryItems = historyItems;
        }
      )
    );
   
  }
}
