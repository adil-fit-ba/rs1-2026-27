namespace MojaApp.API.Modules.Studenti.Commands.Obrisi;

public class ObrisiStudentaCommandHandler(AppDbContext db) : IRequestHandler<ObrisiStudentaCommand>
{
    public async Task Handle(ObrisiStudentaCommand request, CancellationToken ct)
    {
        Student student = await db.Studenti.FirstOrDefaultAsync(x => x.Id == request.Id, ct)
            ?? throw new NotFoundException($"Student (Id = {request.Id}) ne postoji."); // 404

        // Upis ima OnDelete(Restrict). Provjera ovdje daje jasnu poruku (409) umjesto greške iz baze (500).
        if (await db.Upisi.AnyAsync(x => x.StudentId == request.Id, ct))
            throw new ConflictException("Student ima upisane predmete i ne može se obrisati."); // 409

        db.Studenti.Remove(student);
        await db.SaveChangesAsync(ct);
    }
}
