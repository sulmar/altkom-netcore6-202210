namespace Altkom.Net6.RawApi.Middlewares
{

    public static class ApiKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKey(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiKeyMiddleware>();

            return app;
        }
    }


    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Api-Key", out var apiKey) && apiKey == "1234")
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
