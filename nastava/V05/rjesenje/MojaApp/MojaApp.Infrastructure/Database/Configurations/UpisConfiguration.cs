using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MojaApp.Infrastructure.Database.Configurations;

// Upis: Student M:N Predmet, sa svojim podacima (datum upisa, ocjena).
public class UpisConfiguration : IEntityTypeConfiguration<Upis>
{
    public void Configure(EntityTypeBuilder<Upis> builder)
    {
        builder.ToTable("Upisi");

        // Restrict djeluje samo na stvarni DELETE. Studenti i predmeti se brišu soft delete-om (UPDATE),
        // pa studenta ili predmet s upisima čuva provjera u Obrisi*CommandHandler (409).
        builder.HasOne(u => u.Student)
            .WithMany(s => s.Upisi)
            .HasForeignKey(u => u.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Predmet)
            .WithMany(p => p.Upisi)
            .HasForeignKey(u => u.PredmetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Student isti predmet može imati samo jedan neobrisan upis.
        // Filter: nakon ispisa (soft delete) student se na isti predmet može upisati ponovo.
        builder.HasIndex(u => new { u.StudentId, u.PredmetId })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasData(
            new Upis { Id = 1, StudentId = 1, PredmetId = 1, DatumUpisa = new DateTime(2026, 10, 1), CreatedAtUtc = Seed.Datum },
            new Upis { Id = 2, StudentId = 1, PredmetId = 2, DatumUpisa = new DateTime(2026, 2, 20), Ocjena = 8, CreatedAtUtc = Seed.Datum },
            new Upis { Id = 3, StudentId = 2, PredmetId = 1, DatumUpisa = new DateTime(2026, 10, 1), CreatedAtUtc = Seed.Datum });
    }
}
