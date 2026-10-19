using Market.Application.Abstractions;
using Market.Application.Abstractions.Caching;
using Market.Infrastructure.Caching;
using Market.Infrastructure.Common;
using Market.Infrastructure.Database;
using Market.Shared.Constants;
using Market.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Market.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment env)
    {
        // Typed ConnectionStrings + validation
        services.AddOptions<ConnectionStringsOptions>()
            .Bind(configuration.GetSection(ConnectionStringsOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // DbContext: InMemory for automated integration tests; SQL Server otherwise
        services.AddDbContext<DatabaseContext>((sp, options) =>
        {
            if (env.IsIntegrationTests())
            {
                options.UseInMemoryDatabase("IntegrationTestsDb");

                return;
            }

            var cs = sp.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value.Main;
            options.UseSqlServer(cs);
        });

        // IAppDbContext mapping
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<DatabaseContext>());

        // Identity hasher
        services.AddScoped<IPasswordHasher<MarketUserEntity>, PasswordHasher<MarketUserEntity>>();

        // Token service (reads JwtOptions via IOptions<JwtOptions>)
        services.AddTransient<IJwtTokenService, JwtTokenService>();

        // HttpContext accessor + current user
        services.AddHttpContextAccessor();
        services.AddScoped<IAppCurrentUser, AppCurrentUser>();

        // TimeProvider (if used in handlers/services)
        services.AddSingleton<TimeProvider>(TimeProvider.System);

        // Currency conversion (reads rates from the database)
        services.AddScoped<ICurrencyConverter, CurrencyConverter>();

        // Uploaded files (product images) in wwwroot/uploads
        services.AddSingleton<IFileStorage, LocalFileStorage>();

        AddCaching(services, configuration);
        AddExchangeRateProvider(services, configuration);

        return services;
    }

    /// <summary>
    /// Distributed cache: Redis when "ConnectionStrings:Redis" is set, otherwise memory.
    /// ICacheService works the same way on both, so the application starts without a Redis server.
    /// Nothing in the starter uses the cache yet (see ICacheService for an example).
    /// </summary>
    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString(nameof(ConnectionStringsOptions.Redis));

        if (!string.IsNullOrWhiteSpace(redisConnectionString))
        {
            // Connects lazily (on the first cache call), not at startup.
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "Market:";
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        services.AddSingleton<ICacheService, CacheService>();
        services.AddSingleton<ICatalogCacheVersionService, CatalogCacheVersionService>();
    }

    /// <summary>
    /// Optional download of exchange rates ("ExchangeRates" section). The typed HttpClient is only configured here;
    /// the network is used only when an administrator calls POST /Currencies/rates/refresh with Provider = "Frankfurter".
    /// </summary>
    private static void AddExchangeRateProvider(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExchangeRatesOptions>()
            .Bind(configuration.GetSection(ExchangeRatesOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient<IExchangeRateProvider, FrankfurterExchangeRateProvider>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<ExchangeRatesOptions>>().Value;
            client.BaseAddress = new Uri(options.FrankfurterBaseUrl.TrimEnd('/') + "/");
            client.Timeout = TimeSpan.FromSeconds(10);
        });
    }
}
