namespace MojaApp.API.Modules.Predmeti.Queries.GetById;

public class PredmetGetByIdQueryHandler(AppDbContext db) : IRequestHandler<PredmetGetByIdQuery, PredmetResponse>
{
    public async Task<PredmetResponse> Handle(PredmetGetByIdQuery request, CancellationToken ct)
        => await db.Predmeti
               .Where(x => x.Id == request.Id)
               .Select(x => new PredmetResponse
               {
                   Id = x.Id,
                   Naziv = x.Naziv,
                   Sifra = x.Sifra,
                   Ects = x.Ects,
                   BrojUpisanih = x.Upisi.Count, // COUNT u SQL-u
               })
               .FirstOrDefaultAsync(ct)
           ?? throw new NotFoundException($"Predmet (Id = {request.Id}) ne postoji.");
}
