namespace Market.Application.Modules.Catalog.Products.Commands.Create;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(ProductEntity.Constraints.NameMaxLength).WithMessage($"Name can be at most {ProductEntity.Constraints.NameMaxLength} characters long.");

        RuleFor(x => x.Description)
            .MaximumLength(ProductEntity.Constraints.DescriptionMaxLength).WithMessage($"Description can be at most {ProductEntity.Constraints.DescriptionMaxLength} characters long.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category is required.");
    }
}
