namespace MojaApp.API.Modules.Studenti.Commands.Dodaj;

public class DodajStudentaCommandHandler(AppDbContext db) : IRequestHandler<DodajStudentaCommand, StudentResponse>
{
    public async Task<StudentResponse> Handle(DodajStudentaCommand request, CancellationToken ct)
    {
        // Format podataka je već provjerio validator. Ovdje su pravila koja zavise od stanja baze podataka.
        if (request.SmjerId != null && !await db.Smjerovi.AnyAsync(x => x.Id == request.SmjerId, ct))
            throw new NotFoundException($"Smjer (Id = {request.SmjerId}) ne postoji."); // 404

        if (await db.Studenti.AnyAsync(x => x.BrojIndeksa == request.BrojIndeksa, ct))
            throw new ConflictException($"Student s brojem indeksa {request.BrojIndeksa} već postoji."); // 409

        Student student = new()
        {
            Ime = request.Ime,
            Prezime = request.Prezime,
            BrojIndeksa = request.BrojIndeksa,
            DatumRodjenja = request.DatumRodjenja,
            DatumKreiranja = DateTime.UtcNow,
            SmjerId = request.SmjerId,
        };

        db.Studenti.Add(student);
        await db.SaveChangesAsync(ct);

        // Odgovor s nazivom smjera: ista projekcija kao u listi (kao u V03)
        StudentResponse response = await db.Studenti
            .Where(x => x.Id == student.Id)
            .Select(x => new StudentResponse
            {
                Id = x.Id,
                ImePrezime = x.Ime + " " + x.Prezime,
                BrojIndeksa = x.BrojIndeksa,
                DatumRodjenja = x.DatumRodjenja,
                Smjer = x.Smjer != null ? x.Smjer.Naziv : null, // LEFT JOIN Smjerovi
            })
            .FirstAsync(ct);

        return response;
    }
}
