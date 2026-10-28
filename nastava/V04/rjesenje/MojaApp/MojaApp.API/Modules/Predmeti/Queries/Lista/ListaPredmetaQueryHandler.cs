namespace MojaApp.API.Modules.Predmeti.Queries.Lista;

public class ListaPredmetaQueryHandler(AppDbContext db) : IRequestHandler<ListaPredmetaQuery, List<PredmetResponse>>
{
    public Task<List<PredmetResponse>> Handle(ListaPredmetaQuery request, CancellationToken ct)
        => db.Predmeti
            .OrderBy(x => x.Naziv)
            .Select(x => new PredmetResponse
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Sifra = x.Sifra,
                Ects = x.Ects,
                BrojUpisanih = x.Upisi.Count, // COUNT u SQL-u
            })
            .ToListAsync(ct);
}
