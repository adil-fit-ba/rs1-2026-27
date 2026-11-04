using MojaApp.API.Middleware;
using MojaApp.Application;
using MojaApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // Pravila za ulazne podatke su u validatorima (FluentValidation), ne u atributima.
    // Bez ovoga bi ASP.NET svaki string koji nije nullable tretirao kao [Required] i vratio 400 prije validatora.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Svaki sloj registruje svoje servise (kao u starteru):
// Application: MediatR, validatori, ValidationBehavior.
builder.Services.AddApplication();
// Infrastructure: AppDbContext (SQL Server) i IAppDbContext.
builder.Services.AddInfrastructure(builder.Configuration);

// Izuzetak iz handlera postaje HTTP odgovor (400, 404, 409, 500) na jednom mjestu.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Nova baza podataka se kreira i migracije se primjenjuju pri pokretanju.
await app.Services.InitializeDatabaseAsync();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
