using Microsoft.Extensions.DependencyInjection;
using MojaApp.Infrastructure.Database;

namespace MojaApp.Infrastructure;

public static class DatabaseInitializer
{
    // Pri pokretanju primjenjuje migracije koje još nisu u bazi podataka (isto što i Update-Database).
    // Ako baza podataka ne postoji, Migrate je kreira. Starter radi isto (Market.Infrastructure/DatabaseInitializer.cs).
    public static async Task InitializeDatabaseAsync(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }
}
