namespace MojaApp.API.Modules.Upisi.Commands.Ocjena;

public class UpisiOcjenuCommandHandler(AppDbContext db) : IRequestHandler<UpisiOcjenuCommand>
{
    public async Task Handle(UpisiOcjenuCommand request, CancellationToken ct)
    {
        Upis upis = await db.Upisi.FirstOrDefaultAsync(x => x.Id == request.UpisId, ct)
            ?? throw new NotFoundException($"Upis (Id = {request.UpisId}) ne postoji.");

        upis.Ocjena = request.Ocjena;
        await db.SaveChangesAsync(ct);
    }
}
