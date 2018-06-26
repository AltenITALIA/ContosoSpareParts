
import { VehiclesService } from './vehicles.service';
import { vehicle } from '../models/vehicle';
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


let httpClientSpy: { get: jasmine.Spy };
let vehicleService: VehiclesService;


beforeEach(() => {
  // TODO: spy on other methods too
  httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
  vehicleService = new VehiclesService(<any> httpClientSpy, );
});

it('should return expected heroes (HttpClient called once)', () => {
  const expectedVehicle: vehicle[] =
    [{ id: "id", brand: "brand", model:"model", customer:"marco", year:2018, plate:"plate", color:"#color" }];
 
  httpClientSpy.get.and.returnValue(asyncData(expectedVehicle));
 
  vehicleService.getVehicles().subscribe(
    vehicles => expect(vehicles).toEqual(expectedVehicle, 'expected heroes'),
    fail
  );
  expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
});