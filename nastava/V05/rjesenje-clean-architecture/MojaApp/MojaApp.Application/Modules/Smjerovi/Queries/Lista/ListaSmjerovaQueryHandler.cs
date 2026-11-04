namespace MojaApp.Application.Modules.Smjerovi.Queries.Lista;

public class ListaSmjerovaQueryHandler(IAppDbContext db) : IRequestHandler<ListaSmjerovaQuery, List<SmjerResponse>>
{
    public async Task<List<SmjerResponse>> Handle(ListaSmjerovaQuery request, CancellationToken ct)
        => await db.Smjerovi
            .OrderBy(x => x.Naziv)
            .Select(x => new SmjerResponse
            {
                Id = x.Id,
                Naziv = x.Naziv,
                BrojStudenata = x.Studenti.Count, // COUNT u SQL-u
            })
            .ToListAsync(ct);
}
