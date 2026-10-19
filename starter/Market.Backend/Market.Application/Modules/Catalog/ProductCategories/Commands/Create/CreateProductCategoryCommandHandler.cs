namespace Market.Application.Modules.Catalog.ProductCategories.Commands.Create;

public class CreateProductCategoryCommandHandler(IAppDbContext context)
    : IRequestHandler<CreateProductCategoryCommand, int>
{
    public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        // Input format (required name, max length) is checked by CreateProductCategoryCommandValidator.
        var normalized = request.Name.Trim();

        // Check if a category with the same name already exists.
        bool exists = await context.ProductCategories
            .AnyAsync(x => x.Name == normalized, cancellationToken);

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        var category = new ProductCategoryEntity
        {
            Name = normalized,
            IsEnabled = true // default IsEnabled
        };

        context.ProductCategories.Add(category);
        await context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}