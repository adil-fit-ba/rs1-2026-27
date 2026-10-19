using Market.Application.Modules.Auth.Commands.Login;
using Market.Shared.Constants;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Market.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<Program>
{
    // Seeded accounts (see DynamicDataSeeder)
    public const string AdminEmail = "admin@market.local";
    public const string AdminPassword = "Admin123!";
    public const string EmployeeEmail = "employee@market.local";
    public const string EmployeePassword = "Employee123!";

    /// <summary>
    /// JWT signing key used only by integration tests. appsettings.json has an empty key on purpose
    /// (the real key comes from appsettings.Development.json or the environment), so tests supply their own.
    /// </summary>
    private const string TestJwtKey = "INTEGRATION_TESTS_ONLY_KEY_0123456789_ABCDEFGHIJ";

    // Access token per account, so every test does not have to log in again
    private readonly Dictionary<string, string> _cachedTokens = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentNames.IntegrationTests);

        // UseSetting (not ConfigureAppConfiguration): the value is visible already while Program.cs
        // registers services, and AddAPI stops the startup when Jwt:Key is missing.
        builder.UseSetting("Jwt:Key", TestJwtKey);
    }

    /// <summary>
    /// HTTP client with a Bearer token of the given seeded account (admin by default).
    /// </summary>
    public async Task<HttpClient> GetAuthenticatedClientAsync(
        string email = AdminEmail,
        string password = AdminPassword)
    {
        var client = CreateClient();
        if (!_cachedTokens.TryGetValue(email, out var token))
        {
            var loginRequest = new
            {
                Email = email,
                Password = password
            };

            var response = await client.PostAsJsonAsync("api/auth/login", loginRequest);
            response.EnsureSuccessStatusCode();

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginCommandDto>()
                ?? throw new InvalidOperationException("Login response is empty.");

            token = loginResponse.AccessToken;
            _cachedTokens[email] = token;
        }
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
