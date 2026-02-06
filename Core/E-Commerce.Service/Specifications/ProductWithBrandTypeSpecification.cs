using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications;

public class ProductWithBrandTypeSpecification : BaseSpecifications<Product>
{
    public ProductWithBrandTypeSpecification(ProductQueryParameters parameters)
        : base(CreateCriteria(parameters))
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
        ApplyPagination(parameters.PageSize, parameters.PageIndex);
        Sort(parameters);
    }
    public ProductWithBrandTypeSpecification(int id)
        : base(p => p.Id == id)
    {
        AddInclude(p=>p.ProductBrand);
        AddInclude(p => p.ProductType);
    }

    private void Sort(ProductQueryParameters parameters)
    {
        switch (parameters.Sort)
        {
            case ProductSortingOptions.NameAscending:
                AddOrderBy(p => p.Name);
                break;
            case ProductSortingOptions.NameDescending:
                AddOrderByDesc(p => p.Name);
                break;
            case ProductSortingOptions.PriceAscending:
                AddOrderBy(p => p.Price);
                break;
            case ProductSortingOptions.PriceDescending:
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }

    private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
    {
        return p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
                 && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
                 && (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.Contains(parameters.Search));
    }

}
