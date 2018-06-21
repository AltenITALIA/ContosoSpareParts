import { Injectable } from '@angular/core';
import { part } from '../models/part';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment';

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
    )
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); 
      return of(result as T);
    };
  }
}
