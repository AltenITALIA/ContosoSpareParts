
import { fakeAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContosoNavComponent } from './contoso-nav.component';

describe('ContosoNavComponent', () => {
  let component: ContosoNavComponent;
  let fixture: ComponentFixture<ContosoNavComponent>;

  beforeEach(fakeAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ContosoNavComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContosoNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should compile', () => {
    expect(component).toBeTruthy();
  });
});
