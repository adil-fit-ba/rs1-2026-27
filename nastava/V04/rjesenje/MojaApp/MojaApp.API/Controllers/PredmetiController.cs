using Microsoft.AspNetCore.Mvc;
using MojaApp.API.Modules.Predmeti.Commands.Dodaj;
using MojaApp.API.Modules.Predmeti.Commands.Obrisi;
using MojaApp.API.Modules.Predmeti.Queries.Lista;
using MojaApp.API.Modules.Predmeti.Queries.GetById;

namespace MojaApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PredmetiController(ISender sender, AppDbContext db) : ControllerBase
{
    // Zadatak 1: GET /Predmeti
    [HttpGet]
    public Task<List<PredmetResponse>> GetAll(CancellationToken ct)
        => sender.Send(new ListaPredmetaQuery(), ct);

    // Zadatak 1: GET /Predmeti/5 (404 ako ne postoji)
    [HttpGet("{id:int}")]
    public Task<PredmetResponse> GetById(int id, CancellationToken ct)
        => sender.Send(new PredmetGetByIdQuery { Id = id }, ct);

    // Zadatak 2: POST /Predmeti (201; ista šifra 409; neispravni podaci 400)
    [HttpPost]
    public async Task<ActionResult<PredmetResponse>> Dodaj(DodajPredmetCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        PredmetResponse predmet = await sender.Send(new PredmetGetByIdQuery { Id = id }, ct);
        return CreatedAtAction(nameof(GetById), new { id }, predmet);
    }

    // Zadatak 4: DELETE /Predmeti/5 (204; s upisima 409; ne postoji 404)
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Obrisi(int id, CancellationToken ct)
    {
        await sender.Send(new ObrisiPredmetCommand { Id = id }, ct);
        return NoContent();
    }

    // Statistika je ostala iz V03, direktno nad AppDbContext. Prebacivanje u query je isti postupak kao gore.
    [HttpGet("{id:int}/statistika")]
    public async Task<ActionResult<PredmetStatistikaResponse>> Statistika(int id, CancellationToken ct)
    {
        PredmetStatistikaResponse? statistika = await db.Predmeti
            .Where(x => x.Id == id)
            .Select(x => new PredmetStatistikaResponse
            {
                PredmetId = x.Id,
                Naziv = x.Naziv,
                BrojUpisanih = x.Upisi.Count,
                BrojPolozenih = x.Upisi.Count(u => u.Ocjena != null),
                // Average nad praznim skupom: cast na double? daje null umjesto greške
                ProsjecnaOcjena = x.Upisi.Where(u => u.Ocjena != null).Average(u => (double?)u.Ocjena),
            })
            .FirstOrDefaultAsync(ct);

        if (statistika == null)
            return NotFound();

        return statistika;
    }
}
