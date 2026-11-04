using Microsoft.Extensions.DependencyInjection;
using MojaApp.Application.Common.Behaviors;

namespace MojaApp.Application;

// Registracija servisa ovog sloja. Program.cs poziva samo builder.Services.AddApplication().
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly; // ovaj projekat (MojaApp.Application)

        // MediatR: handleri su u ovom projektu, ne u API-ju.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // FluentValidation: svi validatori iz ovog projekta.
        services.AddValidatorsFromAssembly(assembly);

        // Prije svakog handlera MediatR pokreće validatore za taj zahtjev.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
