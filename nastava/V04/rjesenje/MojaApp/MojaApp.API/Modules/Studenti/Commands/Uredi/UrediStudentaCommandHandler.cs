namespace MojaApp.API.Modules.Studenti.Commands.Uredi;

public class UrediStudentaCommandHandler(AppDbContext db) : IRequestHandler<UrediStudentaCommand>
{
    public async Task Handle(UrediStudentaCommand request, CancellationToken ct)
    {
        Student student = await db.Studenti.FirstOrDefaultAsync(x => x.Id == request.Id, ct)
            ?? throw new NotFoundException($"Student (Id = {request.Id}) ne postoji.");

        if (request.SmjerId != null && !await db.Smjerovi.AnyAsync(x => x.Id == request.SmjerId, ct))
            throw new NotFoundException($"Smjer (Id = {request.SmjerId}) ne postoji.");

        student.Ime = request.Ime;
        student.Prezime = request.Prezime;
        student.DatumRodjenja = request.DatumRodjenja;
        student.SmjerId = request.SmjerId;

        await db.SaveChangesAsync(ct);
    }
}
