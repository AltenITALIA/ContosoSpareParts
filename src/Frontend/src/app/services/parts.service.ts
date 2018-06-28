import { Injectable } from '@angular/core';
import { part } from '../models/part';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class PartsService {
  constructor(private httpClient: HttpClient) { }

  getParts(): Observable<part[]> {
    return this.httpClient.get<part[]>(environment.getPartUrl).pipe(
      tap(vehicle => console.info('fetch parts'), 
      catchError(this.handleError('parts', []))
      )
    );
  }
addPart(newPart: part): Observable<ArrayBuffer> {
    return this.httpClient.post<ArrayBuffer>(environment.addPartUrl, newPart, httpOptions).pipe(
      tap(arrayBuffer => console.info('add new part')),
        catchError(this.handleError<ArrayBuffer>('cant add part')
      )
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); 
      return of(result as T);
    };
  }
}
