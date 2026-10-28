using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojaApp.API.Data;
using MojaApp.API.Dtos;

namespace MojaApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SmjeroviController(AppDbContext db) : ControllerBase
{
    // GET /Smjerovi: svaki smjer s brojem studenata
    [HttpGet]
    public async Task<List<SmjerResponse>> GetAll(CancellationToken ct)
    {
        return await db.Smjerovi
            .OrderBy(x => x.Naziv)
            .Select(x => new SmjerResponse
            {
                Id = x.Id,
                Naziv = x.Naziv,
                BrojStudenata = x.Studenti.Count, // COUNT u SQL-u; studenti se ne učitavaju
            })
            .ToListAsync(ct);
    }
}
