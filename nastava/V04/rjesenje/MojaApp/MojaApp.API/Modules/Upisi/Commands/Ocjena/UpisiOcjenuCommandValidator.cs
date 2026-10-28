namespace MojaApp.API.Modules.Upisi.Commands.Ocjena;

public class UpisiOcjenuCommandValidator : AbstractValidator<UpisiOcjenuCommand>
{
    public UpisiOcjenuCommandValidator()
    {
        RuleFor(x => x.Ocjena)
            .InclusiveBetween(6, 10).WithMessage("Ocjena mora biti između 6 i 10.");
    }
}
