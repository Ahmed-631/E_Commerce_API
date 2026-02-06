namespace E_Commerce.Shared.DataTransferObjects.Basket;

public class CustomerBasketDTO
{
    public string Id { get; set; } = default!;

    public ICollection<BasketItemDTO> Items { get; set; } = [];
}
