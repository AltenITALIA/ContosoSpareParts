
import { VehiclesService } from './vehicles.service';
import { Vehicle } from '../models/vehicle';
import { defer } from 'rxjs';

/** Create async observable that emits-once and completes
 *  after a JS engine turn */
export function asyncData<T>(data: T) {
  return defer(() => Promise.resolve(data));
}

/** Create async observable error that errors
 *  after a JS engine turn */
export function asyncError<T>(errorObject: any) {
  return defer(() => Promise.reject(errorObject));
}


let httpClientSpy: { get: jasmine.Spy, post:jasmine.Spy};
let appInsightsServiceSpy : {trackException: jasmine.Spy}
let vehicleService: VehiclesService;


beforeEach(() => {
  // TODO: spy on other methods too
  httpClientSpy = jasmine.createSpyObj('HttpClient', ['post', 'get']);
  appInsightsServiceSpy = jasmine.createSpyObj('AppInsightsService',['trackException'])
  vehicleService = new VehiclesService(<any>httpClientSpy,<any>appInsightsServiceSpy);
});

it('should return expected vehicles (HttpClient called once)', () => {
  const expectedVehicle: Vehicle[] =
    [{ id: "id", brand: "brand", model: "model", customer: "marco", year: 2018, plate: "plate", color: "#color" }];
 
  httpClientSpy.get.and.returnValue(asyncData(expectedVehicle));
  
  vehicleService.getVehicles().subscribe(
    vehicles => expect(vehicles).toEqual(expectedVehicle, 'expected heroes'),
    fail
  );
  expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
});

it('should return expected vehicleId (HttpClient called once)', () => {
  const expectedNewVehicleId: string = "newVehicleID";

  httpClientSpy.post.and.returnValue(asyncData(expectedNewVehicleId));

  vehicleService.addVehicle({ id: "id", brand: "brand", model: "model", customer: "marco", year: 2018, plate: "plate", color: "#color" }).subscribe(
    newVehicleID => expect(newVehicleID).toEqual(expectedNewVehicleId, "expected id"),
     fail
  );
  expect(httpClientSpy.post.calls.count()).toBe(1, 'one call');
  
});