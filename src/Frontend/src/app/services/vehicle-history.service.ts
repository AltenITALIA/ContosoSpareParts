import { Injectable } from '@angular/core';
import { Vehicle } from '../models/vehicle';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of, observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { HistoryItem } from '../models/historyItem';
@Injectable({
  providedIn: 'root'
})
export class VehicleHistoryService {

  constructor(private httpClient: HttpClient) { }
  getHistoryByVehicleId(id:string): Observable<HistoryItem[]> {
    return this.httpClient.get<HistoryItem[]>(`${environment.getHistoryUrl}${id}`).pipe(
      tap(history => console.info('fetch history:',`${environment.getHistoryUrl}${id}`)),
        catchError(this.handleError('cant load history', []))
    ) as Observable<HistoryItem[]>;
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error("error on:",error);
      return of(result as T);
    };
  }

}
