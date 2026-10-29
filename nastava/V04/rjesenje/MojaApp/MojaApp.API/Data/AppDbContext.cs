using Microsoft.EntityFrameworkCore;
using MojaApp.API.Models;

namespace MojaApp.API.Data;

// Most između C# koda i baze podataka: svaki DbSet je jedna tabela.
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Student> Studenti => Set<Student>();
    public DbSet<Smjer> Smjerovi => Set<Smjer>();
    public DbSet<Predmet> Predmeti => Set<Predmet>();
    public DbSet<Upis> Upisi => Set<Upis>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- Smjer 1:N Student ---
        modelBuilder.Entity<Smjer>(e =>
        {
            e.Property(x => x.Naziv).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Smjer)             // student ima jedan smjer
            .WithMany(sm => sm.Studenti)       // smjer ima više studenata
            .HasForeignKey(s => s.SmjerId)     // FK kolona u tabeli Studenti
            .OnDelete(DeleteBehavior.Restrict); // smjer sa studentima se ne može obrisati

        // --- Predmet ---
        modelBuilder.Entity<Predmet>(e =>
        {
            e.Property(x => x.Naziv).HasMaxLength(100).IsRequired();
            e.Property(x => x.Sifra).HasMaxLength(10).IsRequired();
            e.HasIndex(x => x.Sifra).IsUnique(); // dva predmeta ne mogu imati istu šifru
        });

        // --- Upis: Student M:N Predmet ---
        modelBuilder.Entity<Upis>(e =>
        {
            e.HasOne(u => u.Student)
                .WithMany(s => s.Upisi)
                .HasForeignKey(u => u.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // student s upisima se ne briše (čuvaju se ocjene)

            e.HasOne(u => u.Predmet)
                .WithMany(p => p.Upisi)
                .HasForeignKey(u => u.PredmetId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student isti predmet može upisati samo jednom.
            e.HasIndex(u => new { u.StudentId, u.PredmetId }).IsUnique();
        });

        // --- Početni podaci ---
        modelBuilder.Entity<Smjer>().HasData(
            new Smjer { Id = 1, Naziv = "Softverski inženjering" },
            new Smjer { Id = 2, Naziv = "Informacioni sistemi" });

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Ime = "Ana", Prezime = "Anić", BrojIndeksa = "IB260001", DatumRodjenja = new DateOnly(2006, 3, 14), DatumKreiranja = new DateTime(2026, 10, 1), SmjerId = 1 },
            new Student { Id = 2, Ime = "Edin", Prezime = "Edić", BrojIndeksa = "IB260002", DatumKreiranja = new DateTime(2026, 10, 1), SmjerId = 2 });

        modelBuilder.Entity<Predmet>().HasData(
            new Predmet { Id = 1, Naziv = "Razvoj softvera I", Sifra = "RS1", Ects = 7 },
            new Predmet { Id = 2, Naziv = "Programiranje III", Sifra = "PR3", Ects = 6 },
            new Predmet { Id = 3, Naziv = "Baze podataka II", Sifra = "BP2", Ects = 6 });

        modelBuilder.Entity<Upis>().HasData(
            new Upis { Id = 1, StudentId = 1, PredmetId = 1, DatumUpisa = new DateTime(2026, 10, 1) },
            new Upis { Id = 2, StudentId = 1, PredmetId = 2, DatumUpisa = new DateTime(2026, 2, 20), Ocjena = 8 },
            new Upis { Id = 3, StudentId = 2, PredmetId = 1, DatumUpisa = new DateTime(2026, 10, 1) });
    }
}
