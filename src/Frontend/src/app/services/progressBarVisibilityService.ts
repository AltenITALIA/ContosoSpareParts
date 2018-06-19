import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { ContosoHttpInterceptor } from '../services/contoso-http-interceptor'

@Injectable({
    providedIn: 'root'
})
export class ProgressBarVisibilityService {
    private _visibilitySubject: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);

    constructor(private pendingInterceptorService: ContosoHttpInterceptor) {
    }

    /** @deprecated Deprecated in favor of visibilityObservable$ */
    get visibilityObservable(): Observable<boolean> {
        return this._visibilitySubject.asObservable();
    }

    get visibilityObservable$(): Observable<boolean> {
        return this.visibilityObservable;
    }

    public show(): void {
        this.pendingInterceptorService.forceByPass = true;
        this._visibilitySubject.next(true);
        console.info('hide ');
    }

    public hide(): void {
        this._visibilitySubject.next(false);
        this.pendingInterceptorService.forceByPass = false;
        console.info('hide ');
    }
}