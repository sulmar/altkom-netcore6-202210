using Altkom.Net6.RawApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region Lambda
// Warstwa poœrednia (Middleware)
// Logger
//app.Use(async (context, next) =>
//{
//    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");

//    await next();

//    Console.WriteLine($"{context.Response.StatusCode}");
//});

// Warstwa poœrednia (Middleware)
// ApiKey
//app.Use(async (context, next) =>
//{
//    if (context.Request.Headers.TryGetValue("X-Api-Key", out var apiKey) && apiKey == "1234")
//    {
//       await next();       
//    }
//    else
//    {
//        context.Response.StatusCode = 401;
//    }
//});

#endregion

#region UseMiddleware
//app.UseMiddleware<LoggerMiddleware>();
//app.UseMiddleware<ApiKeyMiddleware>();
#endregion

#region Extension Methods

app.UseLogger();
app.UseApiKey();

#endregion


app.Run(async context =>
{
    await context.Response.WriteAsync("Hello World!");
  
});


app.Run();
