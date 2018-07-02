import { Observable, of } from "rxjs";
import { AppInsightsService } from "@markpieszak/ng-application-insights";

export abstract  class BaseService   {
    constructor(private appInsightsService: AppInsightsService) {
        
    }

    protected handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
          console.error(error); 
          this.appInsightsService.trackException(new Error(JSON.stringify(error)), operation);
          return of(result as T);
        };
      }
}