using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MojaApp.API.Common.Behaviors;
using MojaApp.API.Data;
using MojaApp.API.Middleware;

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

// Baza podataka: connection string "Main" iz appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Main")));

// MediatR: pronalazi sve handlere (IRequestHandler) u ovom projektu.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// FluentValidation: registruje sve validatore (AbstractValidator) iz ovog projekta.
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Prije svakog handlera MediatR pokreće validatore za taj zahtjev.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Izuzetak iz handlera postaje HTTP odgovor (400, 404, 409, 500) na jednom mjestu.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

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
