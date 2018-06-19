import { Injectable } from '@angular/core';
import { vehicle } from '../models/vehicle';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class VehiclesService {

  constructor(private httpClient: HttpClient) { }

  getVehicles(): Observable<vehicle[]> {
    return this.httpClient.get<vehicle[]>(environment.getVehicleUrl).pipe(
      tap(vehicle => console.info('fetch veicle'), 
      catchError(this.handleError('vehicles', []))
      )
    )
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error); 

      return of(result as T);
    };
  }
}


