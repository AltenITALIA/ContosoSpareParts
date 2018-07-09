import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ProgressBarVisibilityService } from '../../services/progressBarVisibilityService';
import { ContosoHttpInterceptor } from '../../services/contoso-http-interceptor';
import { EMPTY, merge, Observable, Subscription, timer } from 'rxjs';
import { debounce, delayWhen } from 'rxjs/operators'
@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrls: ['./progress.component.css']
})
export class ProgressComponent implements OnDestroy, OnInit {

  public isProgressVisible: boolean;

  private subscriptions: Subscription;
  private startTime: number;

  @Input()
  public filteredUrlPatterns: string[] = [];
  @Input()
  public filteredMethods: string[] = [];
  @Input()
  public filteredHeaders: string[] = [];
  @Input()
  public debounceDelay = 0;
  @Input()
  public minDuration = 0;
  @Input()
  public entryComponent: any = null

  constructor(private progressService: ProgressBarVisibilityService, private contosoHttpIteceptor: ContosoHttpInterceptor) {
   
    this.subscriptions = merge(
      this.contosoHttpIteceptor.pendingRequestsStatus$.pipe(
        debounce(this.handleDebounceDelay.bind(this)),
        delayWhen(this.handleMinDuration.bind(this))
      ),
      this.progressService.visibilityObservable$,
    ).subscribe(this.handleProgressBarisibility().bind(this));
  }

  ngOnInit(): void {
  
    if (!(this.filteredUrlPatterns instanceof Array)) {
      throw new TypeError('`filteredUrlPatterns` must be an array.');
    }

    if (!!this.filteredUrlPatterns.length) {
      this.filteredUrlPatterns.forEach(e => {
        this.contosoHttpIteceptor.filteredUrlPatterns.push(new RegExp(e));
      });
    }

    if (!(this.filteredMethods instanceof Array)) {
      throw new TypeError('`filteredMethods` must be an array.');
    }
    this.contosoHttpIteceptor.filteredMethods = this.filteredMethods;

    if (!(this.filteredHeaders instanceof Array)) {
      throw new TypeError('`filteredHeaders` must be an array.');
    }
    this.contosoHttpIteceptor.filteredHeaders = this.filteredHeaders;
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  private handleProgressBarisibility(): (v: boolean) => void {
    return v => this.isProgressVisible = v; 
  }

  private handleDebounceDelay(hasPendingRequests: boolean): Observable<number | never> {
    if (hasPendingRequests && !!this.debounceDelay) {
      return timer(this.debounceDelay);
    }
    return EMPTY;
  }

  private handleMinDuration(hasPendingRequests: boolean): Observable<number> {
    if (hasPendingRequests || !this.minDuration) {
      this.startTime = Date.now();
      return timer(0);
    }

    const timerObservable = timer(this.minDuration - (Date.now() - this.startTime));
    this.startTime = null;

    return timerObservable;
  }
}
