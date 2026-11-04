namespace MojaApp.Application.Modules.Predmeti.Queries.Statistika;

// GET /Predmeti/5/statistika (u V04 je bila direktno u kontroleru, nad AppDbContext)
public class PredmetStatistikaQuery : IRequest<PredmetStatistikaResponse>
{
    public int Id { get; set; }
}
