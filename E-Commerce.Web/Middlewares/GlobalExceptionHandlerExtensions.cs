namespace E_Commerce.Web.Middlewares;

public static class GlobalExceptionHandlerExtensions
{
    public static WebApplication UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
        return app;
    }
}
