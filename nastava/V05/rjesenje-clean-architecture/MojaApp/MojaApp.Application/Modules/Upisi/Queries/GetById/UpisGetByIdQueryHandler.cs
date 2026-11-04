namespace MojaApp.Application.Modules.Upisi.Queries.GetById;

public class UpisGetByIdQueryHandler(IAppDbContext db) : IRequestHandler<UpisGetByIdQuery, UpisResponse>
{
    // Upis s imenom studenta i nazivom predmeta: jedan SELECT s dva JOIN-a.
    public async Task<UpisResponse> Handle(UpisGetByIdQuery request, CancellationToken ct)
        => await db.Upisi
               .Where(x => x.Id == request.Id)
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
               .FirstOrDefaultAsync(ct)
           ?? throw new NotFoundException($"Upis (Id = {request.Id}) ne postoji."); // 404
}
