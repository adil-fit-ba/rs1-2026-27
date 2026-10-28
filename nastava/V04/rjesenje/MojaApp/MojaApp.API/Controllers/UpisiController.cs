using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojaApp.API.Data;
using MojaApp.API.Dtos;
using MojaApp.API.Models;
using MojaApp.API.Modules.Upisi.Commands.Ocjena;

namespace MojaApp.API.Controllers;

[ApiController]
[Route("[controller]")]
// Upis, pregled i ispis su ostali iz V03 (direktno nad AppDbContext); samo ocjena ide kroz MediatR.
public class UpisiController(ISender sender, AppDbContext db) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UpisResponse>> GetById(int id, CancellationToken ct)
    {
        UpisResponse? upis = await Ucitaj(id, ct);

        if (upis == null)
            return NotFound();

        return upis;
    }

    // POST /Upisi: upis studenta na predmet
    [HttpPost]
    public async Task<ActionResult<UpisResponse>> Dodaj(UpisDodajRequest request, CancellationToken ct)
    {
        if (!await db.Studenti.AnyAsync(x => x.Id == request.StudentId, ct))
            ModelState.AddModelError(nameof(request.StudentId), "Student ne postoji.");

        if (!await db.Predmeti.AnyAsync(x => x.Id == request.PredmetId, ct))
            ModelState.AddModelError(nameof(request.PredmetId), "Predmet ne postoji.");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState); // 400 s greškom po polju

        // Jedinstven indeks (StudentId, PredmetId) bi odbio duplikat, ali kao izuzetak iz baze (500).
        // Zato provjera prije upisa i 409 s porukom.
        if (await db.Upisi.AnyAsync(x => x.StudentId == request.StudentId && x.PredmetId == request.PredmetId, ct))
            return Conflict("Student je već upisan na ovaj predmet.");

        Upis upis = new()
        {
            StudentId = request.StudentId,
            PredmetId = request.PredmetId,
            DatumUpisa = DateTime.UtcNow,
        };

        db.Upisi.Add(upis);
        await db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = upis.Id }, await Ucitaj(upis.Id, ct));
    }

    // Zadatak 3 (V04): PUT /Upisi/{id}/ocjena kroz MediatR (204; ocjena 4 → 400; ne postoji 404)
    [HttpPut("{id:int}/ocjena")]
    public async Task<IActionResult> UpisiOcjenu(int id, UpisiOcjenuCommand command, CancellationToken ct)
    {
        command.UpisId = id;
        await sender.Send(command, ct);
        return NoContent();
    }

    // DELETE /Upisi/{id}: ispis s predmeta, samo dok ocjena nije upisana
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Obrisi(int id, CancellationToken ct)
    {
        Upis? upis = await db.Upisi.FirstOrDefaultAsync(x => x.Id == id, ct);

        if (upis == null)
            return NotFound();

        if (upis.Ocjena != null)
            return Conflict("Predmet je položen; upis s ocjenom se ne briše.");

        db.Upisi.Remove(upis);
        await db.SaveChangesAsync(ct);
        return NoContent();
    }

    // Upis s imenom studenta i nazivom predmeta: jedan SELECT s dva JOIN-a.
    private Task<UpisResponse?> Ucitaj(int id, CancellationToken ct) =>
        db.Upisi
            .Where(x => x.Id == id)
            .Select(x => new UpisResponse
            {
                Id = x.Id,
                StudentId = x.StudentId,
                Student = x.Student.Ime + " " + x.Student.Prezime, // INNER JOIN Studenti
                PredmetId = x.PredmetId,
                Predmet = x.Predmet.Naziv,                          // INNER JOIN Predmeti
                DatumUpisa = x.DatumUpisa,
                Ocjena = x.Ocjena,
            })
            .FirstOrDefaultAsync(ct);
}
