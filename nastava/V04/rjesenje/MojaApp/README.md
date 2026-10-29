# MojaApp – RS1 vježbe 4, rješenje

ASP.NET Core Web API (.NET 8), EF Core 8, SQL Server. Isti model i iste migracije kao u V03 (`Student`, `Smjer`, `Predmet`, `Upis`), pa radi baza podataka iz V03.

V04 raspoređuje kod po obrascu iz startera za seminarski rad (`Market.Application\Modules\…`):

| Folder | Šta je unutra | U starteru |
|---|---|---|
| `Controllers\` | tanki kontroleri: primaju zahtjev, šalju ga kroz `ISender` (MediatR), vraćaju rezultat | `Market.API\Controllers\` |
| `Modules\<Entitet>\Queries\…` | query (samo čita) i njegov handler | `Market.Application\Modules\…\Queries\` |
| `Modules\<Entitet>\Commands\…` | command (mijenja podatke), handler i validator | `Market.Application\Modules\…\Commands\` |
| `Common\Behaviors\ValidationBehavior.cs` | pokreće validatore prije handlera | `Market.Application\Common\Behaviors\` |
| `Common\Exceptions\` | `NotFoundException` (404), `ConflictException` (409) | `Market.Application\Common\Exceptions\` |
| `Middleware\GlobalExceptionHandler.cs` | izuzetak → HTTP odgovor (ProblemDetails) na jednom mjestu | `MarketExceptionHandler` |
| `Dtos\` | odgovori (`StudentResponse`, `PredmetResponse`, …); projekcija je u `Select` u handleru, kao u V03 | DTO uz svaki query |

Paketi: `MediatR` 12.5.0 (ista verzija kao u starteru; od verzije 13 MediatR ima komercijalnu licencu), `FluentValidation` i `FluentValidation.DependencyInjectionExtensions` 12.2.0.

Kroz MediatR idu:
- `Studenti`: lista s pretragom, detalji s upisima, dodavanje, izmjena (zadatak 5), brisanje;
- `Predmeti`: lista i jedan predmet (zadatak 1), dodavanje (zadatak 2), brisanje (zadatak 4);
- `Upisi`: upis ocjene (zadatak 3).

Statistika predmeta, `Smjerovi` i ostale akcije nad `Upisi` su ostale iz V03, direktno nad `AppDbContext`, da se vidi razlika. Prebacuju se istim postupkom.

Odgovori s greškom (400, 404, 409) su u obliku ProblemDetails (`application/problem+json`). Greške validacije imaju `errors` po polju, kao u V02 i V03.

Testiranje: `MojaApp.API/MojaApp.API.http`. Svi zahtjevi vraćaju iste statuse kao u V03, osim nepostojećeg smjera kod upisa ili izmjene studenta: V03 je vraćao 400, a V04 vraća 404. Validator ne pristupa bazi podataka, pa postojanje smjera provjerava handler (`NotFoundException`), kao `CreateProductCommandHandler` u starteru.
