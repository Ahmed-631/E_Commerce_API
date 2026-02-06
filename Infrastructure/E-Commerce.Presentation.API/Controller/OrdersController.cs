using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controller;

public class OrdersController(IOrderService service)
    : APIBaseController
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(OrderRequest request, CancellationToken cancellationToken = default)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await service.CreateAsync(request, email!, cancellationToken);
        return HandleResult(result);

    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await service.GetByIdAsync(id, email!, cancellationToken);
        return HandleResult(result);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await service.GetByUserEmailAsync(email!, cancellationToken);
        return Ok(result);
    }

    [HttpGet("DeliveryMethods")]
    public async Task<ActionResult<DeliveryMethodResponse>> GetDeliverMethods(CancellationToken cancellationToken = default)
    {
        var result =  await service.GetDeliveryMethodsAsync(cancellationToken);
        return Ok(result);
    }
}
