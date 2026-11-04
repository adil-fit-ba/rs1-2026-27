using Microsoft.AspNetCore.Mvc;
using MojaApp.Application.Modules.Smjerovi.Queries.Lista;

namespace MojaApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SmjeroviController(ISender sender) : ControllerBase
{
    // GET /Smjerovi: svaki smjer s brojem studenata
    [HttpGet]
    public Task<List<SmjerResponse>> GetAll(CancellationToken ct)
        => sender.Send(new ListaSmjerovaQuery(), ct);
}
