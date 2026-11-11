# RS I V06 – Market Backend na .NET 8

Početni kod za vježbu V06 sadrži Web API, pristup bazi podataka i testove. Za rad na vježbi dovoljni su Windows, .NET 8 SDK i SQL Server Express LocalDB (instanca `MSSQLLocalDB`). Swagger služi za pozivanje endpointa.

## Pokretanje

1. Raspakovati ZIP u kratku putanju, npr. `C:\RS1\V06`. U folderu `Market.Backend` nalazi se rješenje `Market.Backend.sln`.
2. Iz tog foldera u terminalu pokrenuti sljedeće komande.

```
dotnet --version
dotnet dev-certs https --trust
dotnet run --project Market.API --launch-profile https
```

   Za `dotnet --version` očekuje se verzija `8.0.x`. U Visual Studiju se može otvoriti rješenje, odabrati `Market.API` kao startup projekat i pokrenuti profil `https`.
3. Otvoriti [Swagger](https://localhost:7100/swagger) i izvršiti `GET /ProductCategories`. API pri prvom pokretanju kreira bazu podataka `Market_V06`, primjenjuje migracije i dodaje demonstracijske podatke. Opcionalni profil `https-testing` koristi zasebnu bazu podataka `Market_V06_Testing`.

Ako je SQL Server već podešen na drugoj instanci, connection string se može zadati varijablom okruženja `ConnectionStrings__Main` prije pokretanja API-ja. Za vježbu odabrati novu bazu podataka.

## Prijava i CRUD nad kategorijama

- Lista `GET /ProductCategories` je javna. Za čitanje jedne kategorije potrebna je prijava, a za kreiranje, izmjenu i brisanje administratorska prava.
- U Swaggeru pozvati `POST /api/auth/login` s demonstracijskim administratorskim računom `admin@market.local` i lozinkom `Admin123!`. Račun `string` s lozinkom `string` služi za demonstraciju korisnika bez administratorskih prava.
- Kopirati vrijednost `accessToken` i unijeti je u dijalog **Authorize**. Za ručno slanje HTTP zahtjeva koristiti zaglavlje `Authorization: Bearer <accessToken>`. Refresh token koristi se za `POST /api/auth/refresh`.
- Kreirati kategoriju pomoću `POST /ProductCategories`, npr. s tijelom `{"name":"V06 primjer"}`. Odgovor `201` sadrži ID koji se može koristiti u narednim zahtjevima za čitanje, izmjenu i brisanje.

## Gdje je kod

| Projekat | Šta se tu nalazi |
|---|---|
| `Market.API` | Kontroleri, JWT autentifikacija i autorizacija, Swagger |
| `Market.Application` | CQRS zahtjevi, handleri, validatori i apstrakcije |
| `Market.Domain` | Entiteti i `BaseEntity` |
| `Market.Infrastructure` | EF Core kontekst, konfiguracije, migracije i JWT servis |
| `Market.Shared` | Zajednički tipovi, opcije i konstante |
| `Market.Tests` | Unit i integracijski testovi |

Primjer za vježbu je u `Market.API/Controllers/ProductCategoriesController.cs` i `Market.Application/Modules/Catalog/ProductCategories`.

## Testovi i migracije

Iz foldera `Market.Backend` testovi se pokreću komandom `dotnet test Market.Backend.sln`.

Za dodavanje migracije nakon promjene modela prvo pokrenuti `dotnet tool restore`. Komanda ispod dodaje novu migraciju.

```
dotnet ef migrations add NazivMigracije --project Market.Infrastructure --startup-project Market.API --output-dir Migrations
```
