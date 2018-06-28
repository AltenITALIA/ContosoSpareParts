import { Injectable } from '@angular/core';
import { vehicle } from '../models/vehicle';
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

  getVehicles(): Observable<vehicle[]> {
    return this.httpClient.get<vehicle[]>(environment.getVehicleUrl).pipe(
      tap(vehicle => console.info('fetch vehicle')),
        catchError(this.handleError('cant load vehicles', []))
    ) as Observable<vehicle[]>;
  }

  addVehicle(newVehicle: vehicle): Observable<string> {
    return this.httpClient.post<string>(environment.addVehicleUrl, newVehicle, httpOptions).pipe(
      tap(newVehicleID => console.info('add new vehicle')),
        catchError(this.handleError<string>('cant add vehicle')
      )
    )
  }
  
  deleteVehicle(vehicleToRemove: vehicle): Observable<vehicle> {
    return this.httpClient.delete<vehicle>(`${environment.deleteVehicleUrl}${vehicleToRemove.id}`, httpOptions).pipe(
      tap(t => console.info("deleted")),
        catchError(this.handleError<vehicle>('cant delete vehicle')
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


