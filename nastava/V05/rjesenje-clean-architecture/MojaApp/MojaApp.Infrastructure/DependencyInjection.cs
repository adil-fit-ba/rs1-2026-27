using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MojaApp.Application.Abstractions;
using MojaApp.Infrastructure.Database;

namespace MojaApp.Infrastructure;

// Registracija servisa ovog sloja. Program.cs poziva builder.Services.AddInfrastructure(builder.Configuration).
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Baza podataka: connection string "Main" iz appsettings.json (projekat MojaApp.API).
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Main")));

        // Handler traži IAppDbContext, a dobija isti AppDbContext (jedan po zahtjevu).
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }
}
