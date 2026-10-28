namespace MojaApp.API.Modules.Studenti.Commands.Uredi;

public class UrediStudentaCommandValidator : AbstractValidator<UrediStudentaCommand>
{
    public UrediStudentaCommandValidator()
    {
        RuleFor(x => x.Ime)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MaximumLength(50).WithMessage("Ime može imati najviše 50 znakova.");

        RuleFor(x => x.Prezime)
            .NotEmpty().WithMessage("Prezime je obavezno.")
            .MaximumLength(50).WithMessage("Prezime može imati najviše 50 znakova.");
    }
}
