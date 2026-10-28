using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MojaApp.API.Common.Exceptions;

namespace MojaApp.API.Middleware;

// Jedno mjesto koje izuzetak pretvara u HTTP odgovor u obliku ProblemDetails.
// Kontroleri i handleri zato nemaju try/catch ni return NotFound()/Conflict().
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext http, Exception ex, CancellationToken ct)
    {
        ProblemDetails problem = ex switch
        {
            // 400: greške po polju, isti oblik kao greške validacije iz V02/V03
            ValidationException v => new ValidationProblemDetails(
                v.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Podaci nisu ispravni.",
            },
            NotFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Nije pronađeno.",
                Detail = ex.Message,
            },
            ConflictException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Konflikt s postojećim podacima.",
                Detail = ex.Message,
            },
            // 500: detalji greške idu u log, ne klijentu
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Greška na serveru.",
            },
        };

        if (problem.Status == StatusCodes.Status500InternalServerError)
            logger.LogError(ex, "Neobrađena greška: {Metoda} {Putanja}", http.Request.Method, http.Request.Path);
        else
            logger.LogWarning("{Status} {Metoda} {Putanja}: {Poruka}",
                problem.Status, http.Request.Method, http.Request.Path, ex.Message);

        http.Response.StatusCode = problem.Status!.Value;
        await http.Response.WriteAsJsonAsync(problem, problem.GetType(), options: null,
            contentType: "application/problem+json", ct);
        return true; // izuzetak je obrađen
    }
}
