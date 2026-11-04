using MojaApp.Application.Abstractions;

namespace MojaApp.Infrastructure.Database;

// Most između C# koda i baze podataka: svaki DbSet odgovara jednoj tabeli.
// Implementira IAppDbContext iz Application, pa handleri ne znaju za ovu klasu ni za SQL Server.
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Student> Studenti => Set<Student>();
    public DbSet<Smjer> Smjerovi => Set<Smjer>();
    public DbSet<Predmet> Predmeti => Set<Predmet>();
    public DbSet<Upis> Upisi => Set<Upis>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracija svakog entiteta je u svojoj klasi (folder Configurations), kao u starteru.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
