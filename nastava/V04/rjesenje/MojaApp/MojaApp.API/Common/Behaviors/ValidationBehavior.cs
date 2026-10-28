using FluentValidation.Results;

namespace MojaApp.API.Common.Behaviors;

// Korak u MediatR pipelineu: prije handlera pokreće sve validatore za taj zahtjev.
// Ako ijedno pravilo ne prođe, handler se ne poziva, nego se baca ValidationException (postaje 400).
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        List<ValidationFailure> greske = [];

        var context = new ValidationContext<TRequest>(request);
        foreach (IValidator<TRequest> validator in validators)
        {
            ValidationResult rezultat = await validator.ValidateAsync(context, ct);
            greske.AddRange(rezultat.Errors);
        }

        if (greske.Count != 0)
            throw new ValidationException(greske);

        return await next();
    }
}
