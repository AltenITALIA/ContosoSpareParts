import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesEditorComponent } from './vehicles-editor.component';

describe('VehiclesEditorComponent', () => {
  let component: VehiclesEditorComponent;
  let fixture: ComponentFixture<VehiclesEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VehiclesEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclesEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
