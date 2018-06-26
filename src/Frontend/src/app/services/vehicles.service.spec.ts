import { TestBed, inject, async } from '@angular/core/testing';
import { VehiclesService } from './vehicles.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatBadgeModule, MatIconRegistry, MatTableModule, MatProgressBarModule, MatInputModule, MatChipsModule, MatDialogModule, MatFormFieldModule, MatDatepickerModule, MatNativeDateModule, MatCheckboxModule, MatSnackBarModule } from '@angular/material';
let httpClientSpy: { get: jasmine.Spy };
let heroService: VehiclesService;

beforeEach(() => {
  // TODO: spy on other methods too
  httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
  heroService = new VehiclesService(<any> httpClientSpy, );
});

describe('VehiclesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VehiclesService]
    });
  });
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientModule, MatSnackBarModule ]
    })
    .compileComponents();
  }));
  it('should be created', inject([VehiclesService], (service: VehiclesService) => {
    expect(service).toBeTruthy();
  }));
});
