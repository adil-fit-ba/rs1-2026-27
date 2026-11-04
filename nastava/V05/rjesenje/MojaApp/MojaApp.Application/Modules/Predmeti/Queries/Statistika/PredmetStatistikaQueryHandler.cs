namespace MojaApp.Application.Modules.Predmeti.Queries.Statistika;

public class PredmetStatistikaQueryHandler(IAppDbContext db) : IRequestHandler<PredmetStatistikaQuery, PredmetStatistikaResponse>
{
    public async Task<PredmetStatistikaResponse> Handle(PredmetStatistikaQuery request, CancellationToken ct)
        => await db.Predmeti
               .Where(x => x.Id == request.Id)
               .Select(x => new PredmetStatistikaResponse
               {
                   PredmetId = x.Id,
                   Naziv = x.Naziv,
                   BrojUpisanih = x.Upisi.Count,
                   BrojPolozenih = x.Upisi.Count(u => u.Ocjena != null),
                   // Average nad praznim skupom: cast na double? daje null umjesto greške
                   ProsjecnaOcjena = x.Upisi.Where(u => u.Ocjena != null).Average(u => (double?)u.Ocjena),
               })
               .FirstOrDefaultAsync(ct)
           ?? throw new NotFoundException($"Predmet (Id = {request.Id}) ne postoji."); // 404
}
