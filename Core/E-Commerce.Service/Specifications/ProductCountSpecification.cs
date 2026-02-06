using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications;

internal sealed class ProductCountSpecification(ProductQueryParameters parameters) 
    : BaseSpecifications<Product>(CreateCriteria(parameters))
{
    private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
    {
        return p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
                 && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
                 && (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.Contains(parameters.Search));
    }
}
