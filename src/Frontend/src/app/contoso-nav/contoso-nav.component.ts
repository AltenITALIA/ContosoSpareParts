import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { MatIconRegistry } from '@angular/material';
import { environment } from '../../environments/environment';

@Component({
  selector: 'contoso-nav',
  templateUrl: './contoso-nav.component.html',
  styleUrls: ['./contoso-nav.component.css']
})
export class ContosoNavComponent {

  public version: string = environment.VERSION;
  
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );

  constructor(private breakpointObserver: BreakpointObserver, iconRegistry: MatIconRegistry) {
  
    iconRegistry.registerFontClassAlias("fontawesome", "fa");
  }

}
