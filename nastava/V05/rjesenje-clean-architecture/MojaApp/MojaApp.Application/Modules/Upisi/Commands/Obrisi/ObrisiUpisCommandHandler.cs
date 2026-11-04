namespace MojaApp.Application.Modules.Upisi.Commands.Obrisi;

public class ObrisiUpisCommandHandler(IAppDbContext db) : IRequestHandler<ObrisiUpisCommand>
{
    public async Task Handle(ObrisiUpisCommand request, CancellationToken ct)
    {
        Upis upis = await db.Upisi.FirstOrDefaultAsync(x => x.Id == request.Id, ct)
            ?? throw new NotFoundException($"Upis (Id = {request.Id}) ne postoji."); // 404

        if (upis.Ocjena != null)
            throw new ConflictException("Predmet je položen; upis s ocjenom se ne briše."); // 409

        db.Upisi.Remove(upis);
        await db.SaveChangesAsync(ct);
    }
}
