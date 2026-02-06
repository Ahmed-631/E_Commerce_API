using E_Commerce.Service.Exceptions;
using E_Commerce.Service.Specifications;
using E_Commerce.ServiceAbstraction.Common;
using E_Commerce.Shared;

namespace E_Commerce.Service.Services;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    : IProductService
{
    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken = default)
    {
        var brands = await unitOfWork.GetRepository<ProductBrand>().GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<BrandResponse>>(brands);
    }

    public async Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.GetRepository<Product>()
            .GetAsync(new ProductWithBrandTypeSpecification(id), cancellationToken);

        if (product is null)
            return Error.NotFound();
        return mapper.Map<ProductResponse>(product);
    }

    public async Task<PaginatedResult<ProductResponse>> GetProductsAsync(ProductQueryParameters parameters, CancellationToken cancellationToken = default)
    {
        var spec = new ProductWithBrandTypeSpecification(parameters);

        var products = await unitOfWork.GetRepository<Product>()
            .GetAllAsync(spec, cancellationToken);

        var data = mapper.Map<IEnumerable<ProductResponse>>(products);

        var totalCount = await unitOfWork.GetRepository<Product>()
            .CountAsync(new ProductCountSpecification(parameters), cancellationToken);

        return new(parameters.PageIndex, data.Count(), totalCount, data);
    }

    public async Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken = default)
    {
        var types = await unitOfWork.GetRepository<ProductType>().GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<TypeResponse>>(types);
    }
}
