using Altkom.Net6.Domain;
using Altkom.Net6.Domain.Validators;
using Altkom.Net6.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // -- rejestracja 
// builder.Services.AddScoped    -- rejestracja DbContext, SqlConnection itp.
// builder.Services.AddTransient -- rejestracja lekkich obiekt�w

builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();

builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

// Warstwa po�rednia (Middleware)
app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");

    await next();

    Console.WriteLine($"{context.Response.StatusCode}");
});

app.MapControllers();

app.Run();
