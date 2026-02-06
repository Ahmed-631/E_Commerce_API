using E_Commerce.Shared.DataTransferObjects.Users;

namespace E_Commerce.Shared.DataTransferObjects.Orders;

public record OrderRequest(AddressDTO Address, string BasketId, int DeliveryMethodId);
