export const environment = {
  production: true,
  getVehicleUrl:'https://contoso-spare-parts.azure-api.net/vehicles/',
  addVehicleUrl:'https://contoso-spare-parts.azure-api.net/vehicles/',
  deleteVehicleUrl:'https://contoso-spare-parts.azure-api.net/vehicles/',
  getPartUrl:'https://contoso-spare-parts.azure-api.net/parts/',
  addPartUrl:'https://contoso-spare-parts.azure-api.net/parts/',
  getHistoryUrl:'https://contoso-spare-parts.azure-api.net/history/byVehicle/',
  getVehicleByPlate:'https://contoso-spare-parts.azure-api.net/vehicles/byPlate/',
  VERSION: require('../../package.json').version
};

