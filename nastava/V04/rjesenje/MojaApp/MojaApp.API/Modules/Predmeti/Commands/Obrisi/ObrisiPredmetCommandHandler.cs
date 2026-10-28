namespace MojaApp.API.Modules.Predmeti.Commands.Obrisi;

public class ObrisiPredmetCommandHandler(AppDbContext db) : IRequestHandler<ObrisiPredmetCommand>
{
    public async Task Handle(ObrisiPredmetCommand request, CancellationToken ct)
    {
        Predmet predmet = await db.Predmeti.FirstOrDefaultAsync(x => x.Id == request.Id, ct)
            ?? throw new NotFoundException($"Predmet (Id = {request.Id}) ne postoji."); // 404

        if (await db.Upisi.AnyAsync(x => x.PredmetId == request.Id, ct))
            throw new ConflictException("Predmet ima upisane studente i ne može se obrisati."); // 409

        db.Predmeti.Remove(predmet);
        await db.SaveChangesAsync(ct);
    }
}
