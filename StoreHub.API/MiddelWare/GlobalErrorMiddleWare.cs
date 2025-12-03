using Shared;

namespace StoreHub.API.MiddelWare
{
    public class GlobalErrorMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorMiddleware> logger;

        public GlobalErrorMiddleware(RequestDelegate _next, ILogger<GlobalErrorMiddleware> _logger)
        {
            next = _next;
            logger = _logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new ApiResponse(500, ex.Message);
                await context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
