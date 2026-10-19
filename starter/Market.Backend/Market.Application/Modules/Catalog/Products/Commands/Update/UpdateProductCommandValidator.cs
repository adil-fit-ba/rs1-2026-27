namespace Market.Application.Modules.Catalog.Products.Commands.Update;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

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
