import { Injectable } from '@angular/core';
import { Part } from '../models/part';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { BaseService } from './base-service';
import { AppInsightsService } from '@markpieszak/ng-application-insights';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class PartsService extends BaseService {
  constructor(private httpClient: HttpClient,  appInsightsService: AppInsightsService) { super(appInsightsService); }

  getParts(): Observable<Part[]> {
    return this.httpClient.get<Part[]>(environment.getPartUrl).pipe(
      tap(vehicle => console.info('fetch parts'),
        catchError(this.handleError('parts', []))
      )
    );
  }
  
  addPart(newPart: Part): Observable<ArrayBuffer> {
    return this.httpClient.post<ArrayBuffer>(environment.addPartUrl, newPart, httpOptions).pipe(
      tap(arrayBuffer => console.info('add new part')),
      catchError(this.handleError<ArrayBuffer>('cant add part')
      )
    );
  }
}
