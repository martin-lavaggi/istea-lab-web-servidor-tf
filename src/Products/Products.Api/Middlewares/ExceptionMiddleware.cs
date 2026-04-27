namespace Products.Api.Middlewares;

/// <summary>
/// Captura las excepciones no manejadas del pipeline, las loguea y devuelve 500
/// con un cuerpo JSON estructurado al cliente.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                error = "Internal server error",
                message = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }
}
