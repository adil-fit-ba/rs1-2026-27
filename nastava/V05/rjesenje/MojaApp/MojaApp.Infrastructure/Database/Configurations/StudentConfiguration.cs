using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MojaApp.Infrastructure.Database.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Studenti");

        // 1:N – student ima jedan smjer, smjer ima više studenata.
        // Restrict djeluje samo na stvarni DELETE; soft delete (UPDATE) ne zaustavlja.
        builder.HasOne(s => s.Smjer)
            .WithMany(sm => sm.Studenti)
            .HasForeignKey(s => s.SmjerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Student { Id = 1, Ime = "Ana", Prezime = "Anić", BrojIndeksa = "IB260001", DatumRodjenja = new DateOnly(2006, 3, 14), SmjerId = 1, CreatedAtUtc = Seed.Datum },
            new Student { Id = 2, Ime = "Edin", Prezime = "Edić", BrojIndeksa = "IB260002", SmjerId = 2, CreatedAtUtc = Seed.Datum });
    }
}
