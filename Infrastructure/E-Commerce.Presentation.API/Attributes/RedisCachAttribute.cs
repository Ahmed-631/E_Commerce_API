using E_Commerce.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace E_Commerce.Presentation.API.Attributes;

internal class RedisCachAttribute(int durationInMinutes)
    : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
        string key = GenerateCashKey(context.HttpContext.Request);
        var cashValue = await cashService.GetAsync(key);
        if (!string.IsNullOrWhiteSpace(cashValue))
        {
            context.Result = new ContentResult
            {
                Content = cashValue,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }

        var result = (await next.Invoke()).Result;
        if (result is OkObjectResult okResult)
        {
            await cashService.SetAsync(key, okResult.Value!, TimeSpan.FromMinutes(durationInMinutes));
        }

        return;
    }

    private static string GenerateCashKey(HttpRequest request)
    {
        var key = new StringBuilder();
        foreach (var kvp in request.Query.OrderBy(q => q.Key))
            key.Append($"{kvp.Key}-{kvp.Value}-");
        return key.ToString().Trim();
    }
}
