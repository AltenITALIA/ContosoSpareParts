import { Injectable } from '@angular/core';
import { Vehicle } from '../models/vehicle';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of, observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { HistoryItem } from '../models/historyItem';
import { BaseService } from './base-service';
import { AppInsightsService } from '@markpieszak/ng-application-insights';
@Injectable({
  providedIn: 'root'
})
export class VehicleHistoryService  extends BaseService  {

  constructor(private httpClient: HttpClient,  appInsightsService: AppInsightsService) { super(appInsightsService); }
  getHistoryByVehicleId(id:string): Observable<HistoryItem[]> {
    return this.httpClient.get<HistoryItem[]>(`${environment.getHistoryUrl}${id}`).pipe(
      tap(history => console.info('fetch history:',`${environment.getHistoryUrl}${id}`)),
        catchError(this.handleError('cant load history', []))
    ) as Observable<HistoryItem[]>;
  }
}
