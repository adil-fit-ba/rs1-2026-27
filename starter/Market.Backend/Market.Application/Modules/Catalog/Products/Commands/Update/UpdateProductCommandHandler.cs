namespace Market.Application.Modules.Catalog.Products.Commands.Update;

public sealed class UpdateProductCommandHandler(IAppDbContext ctx)
            : IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken ct)
    {
        var entity = await ctx.Products
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(ct);

        if (entity is null)
            throw new MarketNotFoundException($"Proizvod (ID={request.Id}) nije pronađen.");

        // Check for duplicate name (case-insensitive, except for the same ID)
        var exists = await ctx.Products
            .AnyAsync(x => x.Id != request.Id && x.Name.ToLower() == request.Name.ToLower(), ct);

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

        entity.Name = request.Name.Trim();
        entity.Description = request.Description?.Trim();
        entity.Price = request.Price;
        entity.CategoryId = request.CategoryId;

        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}