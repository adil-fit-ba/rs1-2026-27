namespace MojaApp.Application.Modules.Upisi.Commands.Dodaj;

// Validator ne pristupa bazi podataka: provjerava samo podatke iz zahtjeva.
// Da li student i predmet postoje, provjerava handler (404).
public class DodajUpisCommandValidator : AbstractValidator<DodajUpisCommand>
{
    public DodajUpisCommandValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Student je obavezan.");
        RuleFor(x => x.PredmetId).GreaterThan(0).WithMessage("Predmet je obavezan.");
    }
}
