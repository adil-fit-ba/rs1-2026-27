using Microsoft.AspNetCore.Mvc;
using MojaApp.Application.Modules.Upisi.Commands.Dodaj;
using MojaApp.Application.Modules.Upisi.Commands.Obrisi;
using MojaApp.Application.Modules.Upisi.Commands.Ocjena;
using MojaApp.Application.Modules.Upisi.Queries.GetById;

namespace MojaApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UpisiController(ISender sender) : ControllerBase
{
    // GET /Upisi/5 (404 ako ne postoji)
    [HttpGet("{id:int}")]
    public Task<UpisResponse> GetById(int id, CancellationToken ct)
        => sender.Send(new UpisGetByIdQuery { Id = id }, ct);

    // POST /Upisi: upis studenta na predmet (201; duplikat 409; student ili predmet ne postoji 404)
    [HttpPost]
    public async Task<ActionResult<UpisResponse>> Dodaj(DodajUpisCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);
        UpisResponse upis = await sender.Send(new UpisGetByIdQuery { Id = id }, ct);
        return CreatedAtAction(nameof(GetById), new { id }, upis);
    }

    // PUT /Upisi/5/ocjena (204; ocjena 4 → 400; ne postoji 404)
    [HttpPut("{id:int}/ocjena")]
    public async Task<IActionResult> UpisiOcjenu(int id, UpisiOcjenuCommand command, CancellationToken ct)
    {
        command.UpisId = id;
        await sender.Send(command, ct);
        return NoContent();
    }

    // DELETE /Upisi/5: ispis s predmeta, samo dok ocjena nije upisana (204; s ocjenom 409)
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Obrisi(int id, CancellationToken ct)
    {
        await sender.Send(new ObrisiUpisCommand { Id = id }, ct);
        return NoContent();
    }
}
