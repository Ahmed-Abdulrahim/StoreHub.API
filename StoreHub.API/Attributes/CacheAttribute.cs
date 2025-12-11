using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;
using Services.Abstraction;
using System.Text;

namespace StoreHub.API.Attributes
{
    public class CacheAttribute(int duration) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheservice = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cacheService;
            var key = GenerateKey(context.HttpContext.Request);
            var result = await cacheservice.GetKey(key);

            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = 200,
                    Content = result
                };
                return;
            }

            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult okResult)
            {
                await cacheservice.SetKey(key, okResult.Value, TimeSpan.FromSeconds(duration));

            }
        }


        private string GenerateKey(HttpRequest context)
        {
            var key = new StringBuilder();

            key.Append(context.Path);
            foreach (var (keyParam, value) in context.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{keyParam}-{value}");
            }
            return key.ToString();
        }
    }
}
