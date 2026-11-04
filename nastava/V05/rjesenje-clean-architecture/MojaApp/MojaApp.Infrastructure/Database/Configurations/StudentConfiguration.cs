using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MojaApp.Infrastructure.Database.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Studenti");

        // 1:N – student ima jedan smjer, smjer ima više studenata.
        builder.HasOne(s => s.Smjer)
            .WithMany(sm => sm.Studenti)
            .HasForeignKey(s => s.SmjerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Student { Id = 1, Ime = "Ana", Prezime = "Anić", BrojIndeksa = "IB260001", DatumRodjenja = new DateOnly(2006, 3, 14), DatumKreiranja = new DateTime(2026, 10, 1), SmjerId = 1 },
            new Student { Id = 2, Ime = "Edin", Prezime = "Edić", BrojIndeksa = "IB260002", DatumKreiranja = new DateTime(2026, 10, 1), SmjerId = 2 });
    }
}
