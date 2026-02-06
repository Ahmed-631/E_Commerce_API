namespace E_Commerce.Shared.DataTransferObjects.Products;

public class ProductQueryParameters
{
    private const int MAXPAGESIZE = 10;
    private const int DEFAULTPAGESIZE = 5;
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Search { get; set; }
    public ProductSortingOptions Sort { get; set; }
    private int pageSize = DEFAULTPAGESIZE;
    public int PageSize
    {
        get => pageSize;
        set => pageSize =
            (value > MAXPAGESIZE) ? MAXPAGESIZE :
            (value < DEFAULTPAGESIZE) ? DEFAULTPAGESIZE :
            value;
    }
    public int PageIndex { get; set; } = 1;
}

public enum ProductSortingOptions
{
    NameAscending = 1,
    NameDescending = 2,
    PriceAscending = 3,
    PriceDescending = 4,
}