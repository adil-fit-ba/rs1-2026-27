# MojaApp – RS1 vježbe 5: postojeći projekat na Clean Architecture (rješenje)

Rješenje V04 raspoređeno u četiri projekta, bez promjene modela i baze podataka:

| Projekat | Sadržaj | Reference |
|---|---|---|
| `MojaApp.Domain` | `Entities\` (Student, Smjer, Predmet, Upis) | nema |
| `MojaApp.Application` | `Abstractions\IAppDbContext.cs`, `Common\`, `Dtos\`, `Modules\`, `DependencyInjection.cs` (`AddApplication`) | Domain |
| `MojaApp.Infrastructure` | `Database\AppDbContext.cs`, `Database\Configurations\`, `Database\Migrations\` (migracije iz V03), `DependencyInjection.cs` (`AddInfrastructure`) | Application, Domain |
| `MojaApp.API` | kontroleri (samo `ISender`), `GlobalExceptionHandler`, `Program.cs`, `appsettings.json` | Application, Infrastructure |

Migracije su samo premještene (isti ID-jevi), pa radi baza podataka iz V03/V04: `dotnet ef migrations list --project MojaApp.Infrastructure --startup-project MojaApp.API` ne prikazuje nijednu migraciju kao Pending, a `has-pending-model-changes` javlja da nema promjena.

Svi kontroleri idu kroz MediatR; u odnosu na V04 su dodani `ListaSmjerovaQuery`, `PredmetStatistikaQuery`, `UpisGetByIdQuery`, `DodajUpisCommand` i `ObrisiUpisCommand`. Promjena ponašanja: `POST /Upisi` s nepostojećim studentom ili predmetom vraća 404 iz handlera (V04: 400).

Postupak korak po korak: mini-prezentacija `V05-postojeci-projekat-na-clean-architecture.html`. Rješenje V05 sa `BaseEntity` i soft delete-om je poseban projekat (nova baza podataka `MojaApp_V05`).
