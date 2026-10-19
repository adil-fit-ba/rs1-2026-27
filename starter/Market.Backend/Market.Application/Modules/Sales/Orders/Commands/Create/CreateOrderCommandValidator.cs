namespace Market.Application.Modules.Sales.Orders.Commands.Create;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item.");

        RuleFor(x => x.Items)
            .Must(items => items.Select(i => i.ProductId).Distinct().Count() == items.Count)
            .WithMessage("The same product cannot appear more than once in an order.")
            .When(x => x.Items is not null);

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId)
                .GreaterThan(0).WithMessage("Product is required.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .Must(q => q == decimal.Truncate(q)).WithMessage("Quantity must be a whole number.");
        });
    }
}
