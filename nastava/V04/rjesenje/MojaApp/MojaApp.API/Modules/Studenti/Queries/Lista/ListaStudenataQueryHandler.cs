namespace MojaApp.API.Modules.Studenti.Queries.Lista;

public class ListaStudenataQueryHandler(AppDbContext db) : IRequestHandler<ListaStudenataQuery, List<StudentResponse>>
{
    public async Task<List<StudentResponse>> Handle(ListaStudenataQuery request, CancellationToken ct)
    {
        IQueryable<Student> upit = db.Studenti;

        if (!string.IsNullOrWhiteSpace(request.Pretraga))
            upit = upit.Where(x => x.Ime.Contains(request.Pretraga)
                                || x.Prezime.Contains(request.Pretraga)
                                || x.BrojIndeksa.Contains(request.Pretraga));

        return await upit
            .OrderBy(x => x.Prezime).ThenBy(x => x.Ime)
            // Projekcija: EF Core pravi jedan SELECT samo s potrebnim kolonama i JOIN-om na Smjerovi
            .Select(x => new StudentResponse
            {
                Id = x.Id,
                ImePrezime = x.Ime + " " + x.Prezime,
                BrojIndeksa = x.BrojIndeksa,
                DatumRodjenja = x.DatumRodjenja,
                Smjer = x.Smjer != null ? x.Smjer.Naziv : null, // LEFT JOIN Smjerovi
            })
            .ToListAsync(ct);
    }
}
