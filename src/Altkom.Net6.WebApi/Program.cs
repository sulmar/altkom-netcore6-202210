using Altkom.Net6.Domain;
using Altkom.Net6.Domain.Validators;
using Altkom.Net6.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // -- rejestracja 
// builder.Services.AddScoped    -- rejestracja DbContext, SqlConnection itp.
// builder.Services.AddTransient -- rejestracja lekkich obiektów

builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();

builder.Services.AddControllers();

// dotnet add package FluentValidation.AspNetCore
builder.Services.AddFluentValidationAutoValidation();

// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

var key = Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = "http://myauthapi.com",
            ValidateAudience = true,
            ValidAudience = "http://myshopper.com"
        };
    });
;

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Warstwa poœrednia (Middleware)
app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");

    await next();

    Console.WriteLine($"{context.Response.StatusCode}");
});

app.MapControllers();

app.Run();
