namespace MojaApp.API.Modules.Studenti.Commands.Dodaj;

// Pravila iz atributa ([Required], [StringLength], [RegularExpression]) su sada ovdje.
// ValidationBehavior pokreće validator prije handlera; ako pravilo ne prođe, handler se ne poziva (400).
// Validator ne pristupa bazi podataka: provjerava samo podatke iz zahtjeva. Pravila nad bazom podataka su u handleru.
public class DodajStudentaCommandValidator : AbstractValidator<DodajStudentaCommand>
{
    public DodajStudentaCommandValidator()
    {
        RuleFor(x => x.Ime)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MaximumLength(50).WithMessage("Ime može imati najviše 50 znakova.");

        RuleFor(x => x.Prezime)
            .NotEmpty().WithMessage("Prezime je obavezno.")
            .MaximumLength(50).WithMessage("Prezime može imati najviše 50 znakova.");

        RuleFor(x => x.BrojIndeksa)
            .Matches(@"^IB\d{6}$").WithMessage("Broj indeksa mora biti u formatu IB123456.");
    }
}
