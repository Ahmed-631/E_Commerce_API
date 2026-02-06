using E_Commerce.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Handlers;

public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        logger.LogError("Something Went Wrong {Message}", ex.Message);

        var problem = new ProblemDetails
        {
            Title = "Error Processing The Http Request",
            Detail = ex.Message,
            Instance = context.Request.Path,
            Status = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            }
        };
        context.Response.StatusCode = problem.Status.Value;
        await context.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }
}
