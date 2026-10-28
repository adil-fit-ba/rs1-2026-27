namespace MojaApp.API.Modules.Studenti.Queries.GetById;

// Student s upisanim predmetima i ocjenama.
public class StudentGetByIdQueryHandler(AppDbContext db) : IRequestHandler<StudentGetByIdQuery, StudentDetaljiResponse>
{
    public async Task<StudentDetaljiResponse> Handle(StudentGetByIdQuery request, CancellationToken ct)
    {
        StudentDetaljiResponse? student = await db.Studenti
            .Where(x => x.Id == request.Id)
            .Select(x => new StudentDetaljiResponse
            {
                Id = x.Id,
                ImePrezime = x.Ime + " " + x.Prezime,
                BrojIndeksa = x.BrojIndeksa,
                Smjer = x.Smjer != null ? x.Smjer.Naziv : null,
                Upisi = x.Upisi
                    .OrderBy(u => u.Predmet.Naziv)
                    .Select(u => new StudentUpisResponse
                    {
                        UpisId = u.Id,
                        Predmet = u.Predmet.Naziv,
                        Sifra = u.Predmet.Sifra,
                        DatumUpisa = u.DatumUpisa,
                        Ocjena = u.Ocjena,
                    })
                    .ToList(),
            })
            .FirstOrDefaultAsync(ct);

        // Umjesto return NotFound(): izuzetak, a GlobalExceptionHandler od njega pravi 404.
        if (student == null)
            throw new NotFoundException($"Student (Id = {request.Id}) ne postoji.");

        return student;
    }
}
