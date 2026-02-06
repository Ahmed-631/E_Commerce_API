using E_Commerce.Presentation.API.Attributes;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DataTransferObjects.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controller;

public class ProductsController(IProductService service)
    : APIBaseController
{
    [RedisCach(2)]
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ProductResponse>>> Get([FromQuery] ProductQueryParameters parameters,CancellationToken cancellationToken = default)
    {
        return Ok(await service.GetProductsAsync(parameters, cancellationToken));
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> Get(int id,CancellationToken cancellationToken = default)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);
        return HandleResult(result);
    }
    [HttpGet("Brands")]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands(CancellationToken cancellationToken = default)
    {
        return Ok(await service.GetBrandsAsync(cancellationToken));
    }
    [HttpGet("Types")]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes(CancellationToken cancellationToken = default)
    {
        return Ok(await service.GetTypesAsync(cancellationToken));
    }
}
