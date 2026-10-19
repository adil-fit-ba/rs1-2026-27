// PRODUKCIJA – ovaj fajl koristi `ng build` (default konfiguracija "production").
//
// Za `ng serve` (konfiguracija "development") Angular ga u buildu zamjenjuje fajlom
// environment.development.ts, a za `ng build --configuration staging` fajlom
// environment.staging.ts (vidi "fileReplacements" u angular.json).
//
// U kodu UVIJEK importuj samo ovaj fajl, nikad .development/.staging direktno:
//   import { environment } from '../../environments/environment';
//
// Sva tri fajla moraju imati ista polja.
export const environment = {
  production: true,
  // TODO: prije deploya postaviti stvarnu adresu produkcijskog API-ja
  apiUrl: 'https://api.example.com'
};
