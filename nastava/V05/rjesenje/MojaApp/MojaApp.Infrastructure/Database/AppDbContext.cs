using System.Linq.Expressions;
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

        PrimijeniGlobalniFilter(modelBuilder);
    }

    // Global query filter: svaki upit nad entitetom koji nasljeđuje BaseEntity dobija WHERE IsDeleted = 0.
    // Vrijedi i za JOIN-ove i podupite (npr. x.Upisi.Count), pa handleri ne pišu !x.IsDeleted.
    // Filter se zaobilazi samo izričito: db.Predmeti.IgnoreQueryFilters().
    private static void PrimijeniGlobalniFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            // Pravi lambdu e => e.IsDeleted == false za konkretan tip (Student, Predmet…).
            var e = Expression.Parameter(entityType.ClrType, "e");
            var uslov = Expression.Equal(
                Expression.Property(e, nameof(BaseEntity.IsDeleted)),
                Expression.Constant(false));

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(uslov, e));
        }
    }

    // Prije svakog snimanja: vrijeme kreiranja i izmjene, a brisanje postaje soft delete.
    // Isto radi starter (Market.Infrastructure/Database/DatabaseConfiguration.cs).
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DateTime sada = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAtUtc = sada;
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedAtUtc = sada;
                    break;

                case EntityState.Deleted:
                    // Remove(...) u handleru: umjesto DELETE ide UPDATE IsDeleted = 1, a red ostaje u tabeli.
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.ModifiedAtUtc = sada;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
