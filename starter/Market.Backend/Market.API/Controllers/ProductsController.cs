using Market.Application.Modules.Catalog.Products.Queries.List;
using Market.Application.Modules.Catalog.Products.Queries.GetById;
using Market.Application.Modules.Catalog.Products.Commands.Create;
using Market.Application.Modules.Catalog.Products.Commands.Delete;
using Market.Application.Modules.Catalog.Products.Commands.Update;
using Market.Shared.Constants;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = PolicyNames.Admin)]
    public async Task<ActionResult<int>> Create(CreateProductCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = PolicyNames.Admin)]
    public async Task Update(int id, UpdateProductCommand command, CancellationToken ct)
    {
        // ID from the route takes precedence
        command.Id = id;
        await sender.Send(command, ct);
        // no return -> 204 No Content
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = PolicyNames.Admin)]
    public async Task Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteProductCommand { Id = id }, ct);
        // no return -> 204 No Content
    }

    // Public catalog: anonymous visitors can browse products (public "search products" page).
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<GetProductByIdQueryDto> GetById(int id, CancellationToken ct)
    {
        var product = await sender.Send(new GetProductByIdQuery { Id = id }, ct);
        return product; // if NotFoundException -> 404 via middleware
    }

    // Public catalog: anonymous visitors can browse products (public "search products" page).
    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ListProductsQueryDto>> List([FromQuery] ListProductsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return result;
    }
}
