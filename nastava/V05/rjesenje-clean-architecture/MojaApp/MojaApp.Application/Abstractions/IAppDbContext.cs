namespace MojaApp.Application.Abstractions;

// Handleri rade s ovim interfejsom, a ne s AppDbContext klasom.
// Implementacija (AppDbContext, SQL Server) je u Infrastructure, pa Application ne zavisi od Infrastructure.
// Isto je u starteru: Market.Application/Abstractions/IAppDbContext.cs.
public interface IAppDbContext
{
    DbSet<Student> Studenti { get; }
    DbSet<Smjer> Smjerovi { get; }
    DbSet<Predmet> Predmeti { get; }
    DbSet<Upis> Upisi { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
