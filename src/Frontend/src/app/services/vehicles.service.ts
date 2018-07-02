import { Injectable } from '@angular/core';
import { Vehicle } from '../models/vehicle';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of, observable } from 'rxjs';
import { environment } from '../../environments/environment';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class VehiclesService {

  constructor(private httpClient: HttpClient) { }

  getVehicles(): Observable<Vehicle[]> {
    return this.httpClient.get<Vehicle[]>(environment.getVehicleUrl).pipe(
      tap(vehicle => console.info('fetch vehicle')),
        catchError(this.handleError('cant load vehicles', []))
    ) as Observable<Vehicle[]>;
  }

  getVehiclesByPlate(plate:string): Observable<Vehicle[]> {
    return this.httpClient.get<Vehicle[]>(`${environment.getVehicleByPlate}${plate}`).pipe(
      tap(vehicle => console.info('fetch vehicle by plate:',`${environment.getVehicleByPlate}${plate}`)),
        catchError(this.handleError('cant load vehicle by plate'))
    ) as Observable<Vehicle[]>;
  }

  addVehicle(newVehicle: Vehicle): Observable<string> {
    return this.httpClient.post<string>(environment.addVehicleUrl, newVehicle, httpOptions).pipe(
      tap(newVehicleID => console.info('add new vehicle')),
        catchError(this.handleError<string>('cant add vehicle')
      )
    )
  }
  
  deleteVehicle(vehicleToRemove: Vehicle): Observable<Vehicle> {
    return this.httpClient.delete<Vehicle>(`${environment.deleteVehicleUrl}${vehicleToRemove.id}`, httpOptions).pipe(
      tap(t => console.info("deleted")),
        catchError(this.handleError<Vehicle>('cant delete vehicle')
      )
    )
  }
  
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error("error on:",error);
      return of(result as T);
    };
  }
}


