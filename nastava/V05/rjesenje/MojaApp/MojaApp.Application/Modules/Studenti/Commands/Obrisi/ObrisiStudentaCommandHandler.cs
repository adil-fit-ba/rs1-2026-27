namespace MojaApp.Application.Modules.Studenti.Commands.Obrisi;

public class ObrisiStudentaCommandHandler(IAppDbContext db) : IRequestHandler<ObrisiStudentaCommand>
{
    public async Task Handle(ObrisiStudentaCommand request, CancellationToken ct)
    {
        Student student = await db.Studenti.FirstOrDefaultAsync(x => x.Id == request.Id, ct)
            ?? throw new NotFoundException($"Student (Id = {request.Id}) ne postoji."); // 404

        // Soft delete je UPDATE, pa OnDelete(Restrict) na Upis više ne sprječava brisanje.
        // Ova provjera je zato jedina zaštita: student s upisima se ne briše (čuvaju se ocjene).
        // Ispisani (soft-obrisani) upisi se ne broje, jer ih global query filter ne vraća.
        if (await db.Upisi.AnyAsync(x => x.StudentId == request.Id, ct))
            throw new ConflictException("Student ima upisane predmete i ne može se obrisati."); // 409

        db.Studenti.Remove(student); // soft delete: AppDbContext pravi UPDATE IsDeleted = 1
        await db.SaveChangesAsync(ct);
    }
}
