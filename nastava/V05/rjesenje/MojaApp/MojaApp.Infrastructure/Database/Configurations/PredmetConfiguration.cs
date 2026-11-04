using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MojaApp.Infrastructure.Database.Configurations;

public class PredmetConfiguration : IEntityTypeConfiguration<Predmet>
{
    public void Configure(EntityTypeBuilder<Predmet> builder)
    {
        builder.ToTable("Predmeti");
        builder.Property(x => x.Naziv).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Sifra).HasMaxLength(10).IsRequired();

        // Dva neobrisana predmeta ne mogu imati istu šifru.
        // Filter: obrisani (IsDeleted = 1) predmet ostaje u tabeli, pa bi bez filtera
        // njegova šifra ostala zauzeta i novi predmet s istom šifrom bi pao na indeksu.
        builder.HasIndex(x => x.Sifra)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasData(
            new Predmet { Id = 1, Naziv = "Razvoj softvera I", Sifra = "RS1", Ects = 7, CreatedAtUtc = Seed.Datum },
            new Predmet { Id = 2, Naziv = "Programiranje III", Sifra = "PR3", Ects = 6, CreatedAtUtc = Seed.Datum },
            new Predmet { Id = 3, Naziv = "Baze podataka II", Sifra = "BP2", Ects = 6, CreatedAtUtc = Seed.Datum });
    }
}
