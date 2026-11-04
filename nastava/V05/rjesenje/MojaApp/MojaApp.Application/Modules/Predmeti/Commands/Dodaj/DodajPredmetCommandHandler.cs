namespace MojaApp.Application.Modules.Predmeti.Commands.Dodaj;

public class DodajPredmetCommandHandler(IAppDbContext db) : IRequestHandler<DodajPredmetCommand, int>
{
    public async Task<int> Handle(DodajPredmetCommand request, CancellationToken ct)
    {
        // Obrisani (IsDeleted) predmet se ne broji (global query filter), pa se njegova šifra može ponovo koristiti.
        // Zato je i jedinstveni indeks na Sifra filtriran (AppDbContext: HasFilter("[IsDeleted] = 0")).
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
