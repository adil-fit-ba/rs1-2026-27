using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MojaApp.Infrastructure.Database.Configurations;

public class SmjerConfiguration : IEntityTypeConfiguration<Smjer>
{
    public void Configure(EntityTypeBuilder<Smjer> builder)
    {
        builder.ToTable("Smjerovi");
        builder.Property(x => x.Naziv).HasMaxLength(100).IsRequired();

        builder.HasData(
            new Smjer { Id = 1, Naziv = "Softverski inženjering", CreatedAtUtc = Seed.Datum },
            new Smjer { Id = 2, Naziv = "Informacioni sistemi", CreatedAtUtc = Seed.Datum });
    }
}
