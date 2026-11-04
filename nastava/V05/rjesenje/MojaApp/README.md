# MojaApp – RS1 vježbe 5, rješenje

ASP.NET Core Web API (.NET 8), EF Core 8, SQL Server, MediatR 12.5.0 i FluentValidation 12.2.0, kao u V04.

V05 dijeli rješenje V04 na četiri projekta (Clean Architecture). Uvodi i `BaseEntity` sa soft delete-om i global query filterom. Oba obrasca su iz startera za seminarski rad (`Market.*`).

## Projekti i zavisnosti

```
MojaApp.API ──► MojaApp.Application ──► MojaApp.Domain
     │                  ▲
     └──► MojaApp.Infrastructure ─┘ (i ► Domain)
```

| Projekat | Šta je unutra | Zavisi od | U starteru |
|---|---|---|---|
| `MojaApp.Domain` | entiteti (`Student`, `Smjer`, `Predmet`, `Upis`) i `Common\BaseEntity.cs` | ničega | `Market.Domain` |
| `MojaApp.Application` | `Modules\…` (commands, queries, handleri, validatori), `Dtos\`, `Common\` (izuzeci, `ValidationBehavior`), `Abstractions\IAppDbContext.cs`, `DependencyInjection.cs` (`AddApplication`) | Domain | `Market.Application` |
| `MojaApp.Infrastructure` | `Database\AppDbContext.cs` (soft delete, global query filter), `Database\Configurations\` (po jedna klasa po entitetu), `Database\Migrations\`, `DependencyInjection.cs` (`AddInfrastructure`), `DatabaseInitializer.cs` | Application, Domain | `Market.Infrastructure` |
| `MojaApp.API` | kontroleri, `Middleware\GlobalExceptionHandler.cs`, `Program.cs` | Application, Infrastructure | `Market.API` |

`Market.Shared` iz startera ovdje nema. DTO su u Application.

Handleri rade s interfejsom `IAppDbContext`, a ne s klasom `AppDbContext`. Implementacija je u Infrastructure, pa Application ne zna za SQL Server. API referencira Infrastructure samo zbog registracije u `Program.cs`. Nijedan kontroler nema `AppDbContext`. U V04 su statistika predmeta, `Smjerovi` i većina akcija nad `Upisi` bili direktno nad `AppDbContext`, a sada su queries i commands.

## BaseEntity i soft delete

- Svi entiteti nasljeđuju `BaseEntity` (`Id`, `IsDeleted`, `CreatedAtUtc`, `ModifiedAtUtc`). `CreatedAtUtc` zamjenjuje `DatumKreiranja` iz V02–V04.
- `AppDbContext.SaveChangesAsync` postavlja `CreatedAtUtc` i `ModifiedAtUtc`, a `Remove(...)` pretvara u `UPDATE … IsDeleted = 1`. Red ostaje u tabeli.
- Global query filter dodaje `WHERE IsDeleted = 0` na svaki upit nad tim entitetima, i u JOIN-ovima i podupitima (`x.Upisi.Count`, `x.Smjer.Naziv`). Obrisan zapis se zato ponaša kao nepostojeći (404).
- Jedinstveni indeksi `Predmeti.Sifra` i `Upisi (StudentId, PredmetId)` imaju `HasFilter("[IsDeleted] = 0")`. Tako se šifra obrisanog predmeta može ponovo koristiti, a student se nakon ispisa može ponovo upisati.
- `OnDelete(Restrict)` djeluje samo na stvarni DELETE. Soft delete je UPDATE, pa studenta i predmet s upisima čuvaju samo provjere u `ObrisiStudentaCommandHandler` i `ObrisiPredmetCommandHandler` (409).

## Baza podataka i migracije

V05 radi na novoj bazi podataka `MojaApp_V05` (connection string u `MojaApp.API\appsettings.json`). Migracije iz V02–V04 su obrisane, a u `MojaApp.Infrastructure\Database\Migrations` je jedna početna migracija `Pocetna`.

Pri pokretanju `Program.cs` poziva `InitializeDatabaseAsync()`, koji primjenjuje migracije (`Database.MigrateAsync()`). Ako baza podataka ne postoji, kreira se. `Update-Database` zato nije potreban, ali i dalje radi.

Nova migracija se pravi ovako:

Package Manager Console (Default project: `MojaApp.Infrastructure`, startup project: `MojaApp.API`):

```
Add-Migration NazivMigracije -Project MojaApp.Infrastructure -StartupProject MojaApp.API -OutputDir Database\Migrations
```

Terminal (folder rješenja):

```
dotnet ef migrations add NazivMigracije --project MojaApp.Infrastructure --startup-project MojaApp.API --output-dir Database/Migrations
```

## Testiranje

`MojaApp.API/MojaApp.API.http`. Statusi su isti kao u V04, osim upisa nepostojećeg studenta ili predmeta: V04 je vraćao 400 (provjera u kontroleru), a V05 vraća 404 iz `DodajUpisCommandHandler`. Sekcija V05 na kraju fajla pokazuje soft delete korak po korak.
