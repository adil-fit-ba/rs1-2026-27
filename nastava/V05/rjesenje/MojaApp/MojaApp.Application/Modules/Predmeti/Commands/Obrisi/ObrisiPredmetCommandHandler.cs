namespace MojaApp.Application.Modules.Predmeti.Commands.Obrisi;

public class ObrisiPredmetCommandHandler(IAppDbContext db) : IRequestHandler<ObrisiPredmetCommand>
{
    public async Task Handle(ObrisiPredmetCommand request, CancellationToken ct)
    {
        Predmet predmet = await db.Predmeti.FirstOrDefaultAsync(x => x.Id == request.Id, ct)
            ?? throw new NotFoundException($"Predmet (Id = {request.Id}) ne postoji."); // 404

        // Soft delete je UPDATE, pa OnDelete(Restrict) na Upis više ne sprječava brisanje.
        // Ova provjera je zato jedina zaštita.
        if (await db.Upisi.AnyAsync(x => x.PredmetId == request.Id, ct))
            throw new ConflictException("Predmet ima upisane studente i ne može se obrisati."); // 409

        db.Predmeti.Remove(predmet); // soft delete: AppDbContext pravi UPDATE IsDeleted = 1
        await db.SaveChangesAsync(ct);
    }
}
