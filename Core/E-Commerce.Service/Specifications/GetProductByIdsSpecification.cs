namespace E_Commerce.Service.Specifications;

public class GetProductByIdsSpecification(List<int> ids) 
    : BaseSpecifications<Product>(p => ids.Contains(p.Id));