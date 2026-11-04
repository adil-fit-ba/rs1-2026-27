using Microsoft.AspNetCore.Mvc;
using MojaApp.Application.Modules.Predmeti.Commands.Dodaj;
using MojaApp.Application.Modules.Predmeti.Commands.Obrisi;
using MojaApp.Application.Modules.Predmeti.Queries.GetById;
using MojaApp.Application.Modules.Predmeti.Queries.Lista;
using MojaApp.Application.Modules.Predmeti.Queries.Statistika;

namespace MojaApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PredmetiController(ISender sender) : ControllerBase
{
    // GET /Predmeti
    [HttpGet]
    public Task<List<PredmetResponse>> GetAll(CancellationToken ct)
        => sender.Send(new ListaPredmetaQuery(), ct);

    // GET /Predmeti/5 (404 ako ne postoji ili je obrisan)
    [HttpGet("{id:int}")]
    public Task<PredmetResponse> GetById(int id, CancellationToken ct)
        => sender.Send(new PredmetGetByIdQuery { Id = id }, ct);

    // POST /Predmeti (201; ista šifra 409; neispravni podaci 400)
    [HttpPost]
    public async Task<ActionResult<PredmetResponse>> Dodaj(DodajPredmetCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        PredmetResponse predmet = await sender.Send(new PredmetGetByIdQuery { Id = id }, ct);
        return CreatedAtAction(nameof(GetById), new { id }, predmet);
    }

    // DELETE /Predmeti/5 (204 soft delete; s upisima 409; ne postoji 404)
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Obrisi(int id, CancellationToken ct)
    {
        await sender.Send(new ObrisiPredmetCommand { Id = id }, ct);
        return NoContent();
    }

    // GET /Predmeti/5/statistika (u V04 direktno nad AppDbContext, sada query)
    [HttpGet("{id:int}/statistika")]
    public Task<PredmetStatistikaResponse> Statistika(int id, CancellationToken ct)
        => sender.Send(new PredmetStatistikaQuery { Id = id }, ct);
}
