namespace MojaApp.API.Modules.Predmeti.Commands.Dodaj;

public class DodajPredmetCommandValidator : AbstractValidator<DodajPredmetCommand>
{
    public DodajPredmetCommandValidator()
    {
        RuleFor(x => x.Naziv)
            .NotEmpty().WithMessage("Naziv je obavezan.")
            .MaximumLength(100).WithMessage("Naziv može imati najviše 100 znakova.");

        RuleFor(x => x.Sifra)
            .NotEmpty().WithMessage("Šifra je obavezna.")
            .MaximumLength(10).WithMessage("Šifra može imati najviše 10 znakova.");

        RuleFor(x => x.Ects)
            .InclusiveBetween(1, 30).WithMessage("ECTS mora biti između 1 i 30.");
    }
}
