using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Basket;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controller;

public class BasketsController(IBasketService basketService)
    : APIBaseController
{
    [HttpPost]
    public async Task<ActionResult<CustomerBasketDTO>> Update(CustomerBasketDTO customerBasket)
        => Ok(await basketService.CreateOrUpdateAsync(customerBasket));
    [HttpGet]
    public async Task<ActionResult<CustomerBasketDTO>> Get(string id)
        => Ok(await basketService.GetByIdAsync(id));
    [HttpDelete]
    public async Task<ActionResult> Delete(string id)
    {
        await basketService.DeleteAsync(id);
        return NoContent();
    }
}
