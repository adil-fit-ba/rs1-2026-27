using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MojaApp.Infrastructure.Database.Configurations;

public class PredmetConfiguration : IEntityTypeConfiguration<Predmet>
{
    public void Configure(EntityTypeBuilder<Predmet> builder)
    {
        builder.ToTable("Predmeti");
        builder.Property(x => x.Naziv).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Sifra).HasMaxLength(10).IsRequired();

        // Dva predmeta ne mogu imati istu šifru.
        builder.HasIndex(x => x.Sifra)
            .IsUnique();

        builder.HasData(
            new Predmet { Id = 1, Naziv = "Razvoj softvera I", Sifra = "RS1", Ects = 7 },
            new Predmet { Id = 2, Naziv = "Programiranje III", Sifra = "PR3", Ects = 6 },
            new Predmet { Id = 3, Naziv = "Baze podataka II", Sifra = "BP2", Ects = 6 });
    }
}
