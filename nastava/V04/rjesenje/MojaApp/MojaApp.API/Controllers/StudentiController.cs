using Microsoft.AspNetCore.Mvc;
using MojaApp.API.Modules.Studenti.Commands.Dodaj;
using MojaApp.API.Modules.Studenti.Commands.Obrisi;
using MojaApp.API.Modules.Studenti.Commands.Uredi;
using MojaApp.API.Modules.Studenti.Queries.Lista;
using MojaApp.API.Modules.Studenti.Queries.GetById;

namespace MojaApp.API.Controllers;

// Tanak kontroler: prima HTTP zahtjev, šalje query/command kroz MediatR i vraća rezultat.
// Nema AppDbContext, nema provjera, nema NotFound()/Conflict(): to je u handlerima i GlobalExceptionHandler-u.
[ApiController]
[Route("[controller]")]
public class StudentiController(ISender sender) : ControllerBase
{
    // GET /Studenti?pretraga=ana
    [HttpGet]
    public Task<List<StudentResponse>> GetAll([FromQuery] ListaStudenataQuery query, CancellationToken ct)
        => sender.Send(query, ct);

    // GET /Studenti/5: student s upisanim predmetima i ocjenama (404 ako ne postoji)
    [HttpGet("{id:int}")]
    public Task<StudentDetaljiResponse> GetById(int id, CancellationToken ct)
        => sender.Send(new StudentGetByIdQuery { Id = id }, ct);

    [HttpPost]
    public async Task<ActionResult<StudentResponse>> Dodaj(DodajStudentaCommand command, CancellationToken ct)
    {
        StudentResponse student = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student); // 201
    }

    // Zadatak 5 (dodatno)
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Uredi(int id, UrediStudentaCommand command, CancellationToken ct)
    {
        command.Id = id; // Id iz rute ima prednost
        await sender.Send(command, ct);
        return NoContent(); // 204
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Obrisi(int id, CancellationToken ct)
    {
        await sender.Send(new ObrisiStudentaCommand { Id = id }, ct);
        return NoContent(); // 204
    }
}
