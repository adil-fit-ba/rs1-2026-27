namespace MojaApp.Application.Modules.Upisi.Commands.Dodaj;

public class DodajUpisCommandHandler(IAppDbContext db) : IRequestHandler<DodajUpisCommand, int>
{
    public async Task<int> Handle(DodajUpisCommand request, CancellationToken ct)
    {
        // Pravila nad bazom podataka su u handleru; validator provjerava samo podatke iz zahtjeva.
        if (!await db.Studenti.AnyAsync(x => x.Id == request.StudentId, ct))
            throw new NotFoundException($"Student (Id = {request.StudentId}) ne postoji."); // 404

        if (!await db.Predmeti.AnyAsync(x => x.Id == request.PredmetId, ct))
            throw new NotFoundException($"Predmet (Id = {request.PredmetId}) ne postoji."); // 404

        // Jedinstveni indeks (StudentId, PredmetId) bi odbio duplikat, ali kao izuzetak iz baze podataka (500).
        if (await db.Upisi.AnyAsync(x => x.StudentId == request.StudentId && x.PredmetId == request.PredmetId, ct))
            throw new ConflictException("Student je već upisan na ovaj predmet."); // 409

        Upis upis = new()
        {
            StudentId = request.StudentId,
            PredmetId = request.PredmetId,
            DatumUpisa = DateTime.UtcNow,
        };

        db.Upisi.Add(upis);
        await db.SaveChangesAsync(ct);

        return upis.Id;
    }
}
