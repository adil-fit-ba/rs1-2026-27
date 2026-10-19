// STAGING – koristi se za `ng build --configuration staging`.
// Builda se kao produkcija (optimizovano, bez razvojnih pomagala), samo prema drugom API-ju.
// Angular ovim fajlom zamjenjuje environment.ts (vidi "fileReplacements" u angular.json).
export const environment = {
  production: true,
  // TODO: prije deploya postaviti stvarnu adresu staging API-ja
  apiUrl: 'https://staging-api.example.com'
};
