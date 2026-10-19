namespace Market.Application.Modules.Catalog.Products.Commands.Create;

public class CreateProductCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken ct)
    {
        // Input format (required name, lengths, price...) is checked by CreateProductCommandValidator.
        var normalized = request.Name.Trim();

        // Check if a product with the same name already exists.
        bool exists = await ctx.Products
            .AnyAsync(x => x.Name == normalized, ct);

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        var productCategory = await ctx.ProductCategories
            .Where(x => x.Id == request.CategoryId)
            .FirstOrDefaultAsync(ct);

        if (productCategory is null)
        {
            throw new MarketNotFoundException($"Kategorija (ID={request.CategoryId}) nije pronađena.");
        }

        if (!productCategory.IsEnabled)
        {
            throw new MarketBusinessRuleException("product.category.disabled",
                $"Kategorija {productCategory.Name} je onemogućena i ne može se dodijeliti proizvodu.");
        }

        var product = new ProductEntity
        {
            Name = normalized,
            Description = request.Description?.Trim(),
            Price = request.Price,
            StockQuantity = 0, // default StockQuantity
            CategoryId = request.CategoryId,
            IsEnabled = true // default IsEnabled
        };

        ctx.Products.Add(product);
        await ctx.SaveChangesAsync(ct);

        return product.Id;
    }
}
