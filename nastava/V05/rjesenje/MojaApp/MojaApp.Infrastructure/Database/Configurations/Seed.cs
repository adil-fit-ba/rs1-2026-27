namespace MojaApp.Infrastructure.Database.Configurations;

// HasData traži fiksne vrijednosti (DateTime.UtcNow bi svaku migraciju činio drugačijom).
internal static class Seed
{
    public static readonly DateTime Datum = new(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc);
}
