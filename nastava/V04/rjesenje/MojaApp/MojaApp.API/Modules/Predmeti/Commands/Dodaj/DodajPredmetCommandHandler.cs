namespace MojaApp.API.Modules.Predmeti.Commands.Dodaj;

public class DodajPredmetCommandHandler(AppDbContext db) : IRequestHandler<DodajPredmetCommand, int>
{
    public async Task<int> Handle(DodajPredmetCommand request, CancellationToken ct)
    {
        if (await db.Predmeti.AnyAsync(x => x.Sifra == request.Sifra, ct))
            throw new ConflictException($"Predmet sa šifrom {request.Sifra} već postoji."); // 409

        Predmet predmet = new()
        {
            Naziv = request.Naziv,
            Sifra = request.Sifra,
            Ects = request.Ects,
        };

        db.Predmeti.Add(predmet);
        await db.SaveChangesAsync(ct);
        return predmet.Id;
    }
}
