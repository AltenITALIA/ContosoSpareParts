import { HTTP_INTERCEPTORS, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { ExistingProvider, Injectable } from '@angular/core';
import { Observable, ReplaySubject, throwError } from 'rxjs';
import { catchError, finalize, map } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class ContosoHttpInterceptor  implements HttpInterceptor  {
    private _pendingRequests = 0;
    private _pendingRequestsStatus: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);
    private _filteredUrlPatterns: RegExp[] = [];
    private _filteredMethods: string[] = [];
    private _filteredHeaders: string[] = [];
    private _forceByPass: boolean;

    /** @deprecated Deprecated in favor of pendingRequestsStatus$ */
    get pendingRequestsStatus(): Observable<boolean> {
        return this._pendingRequestsStatus.asObservable();
    }

    get pendingRequestsStatus$(): Observable<boolean> {
        return this.pendingRequestsStatus;
    }
    get pendingRequests(): number {
        return this._pendingRequests;
    }

    get filteredUrlPatterns(): RegExp[] {
        return this._filteredUrlPatterns;
    }

    set filteredMethods(httpMethods: string[]) {
        this._filteredMethods = httpMethods;
    }

    set filteredHeaders(value: string[]) {
        this._filteredHeaders = value;
    }

    set forceByPass(value: boolean) {
        this._forceByPass = value;
    }

    private shouldBypassUrl(url: string): boolean {
        return this._filteredUrlPatterns.some(e => {
            return e.test(url);
        });
    }

    private shouldBypassMethod(req: HttpRequest<any>): boolean {
        return this._filteredMethods.some(e => {
            return e.toUpperCase() === req.method.toUpperCase();
        });
    }

    private shouldBypassHeader(req: HttpRequest<any>): boolean {
        return this._filteredHeaders.some(e => {
            return req.headers.has(e);
        });
    }

    private shouldBypass(req: HttpRequest<any>): boolean {
        return this.shouldBypassUrl(req.urlWithParams)
            || this.shouldBypassMethod(req)
            || this.shouldBypassHeader(req)
            || this._forceByPass;
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const shouldBypass = this.shouldBypass(req);

        if (!shouldBypass) {
            this._pendingRequests++;

            if (1 === this._pendingRequests) {
                this._pendingRequestsStatus.next(true);
                
            }
        }

        return next.handle(req).pipe(
            map(event => {
                return event;
            }),
            catchError(error => {
                return throwError(error);
            }),
            finalize(() => {
                if (!shouldBypass) {
                    this._pendingRequests--;
                    if (0 === this._pendingRequests) {
                        this._pendingRequestsStatus.next(false);
                    }
                }
            })
        );
    }
}

