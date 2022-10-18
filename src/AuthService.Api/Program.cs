using AuthService.Api.Models;
using AuthService.Domain;
using AuthService.Infrastructure;
using Bogus;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Faker<User>, UserFaker>();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IAuthService, MyAuthService>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();

builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

app.MapPost("api/token/create", (AuthModel model, IAuthService authService, ITokenService tokenService) =>
{
    if (authService.TryAuthorize(model.Login, model.Password, out User user))
    {
        var token = tokenService.Create(user);

        return Results.Ok(token);
    }
    else
    {
        return Results.BadRequest(new { message = "Username or password is incorrect." });
    }
}).WithName("CreateToken");


app.MapGet("/", (LinkGenerator linkGenerator) => $"Use POST {linkGenerator.GetPathByName("CreateToken", null)} {JsonSerializer.Serialize(new AuthModel())}");

app.Run();
