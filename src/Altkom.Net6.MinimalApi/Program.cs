using Altkom.Net6.Domain;
using Altkom.Net6.Infrastructure;
using Altkom.Net6.MinimalApi;
using Microsoft.AspNetCore.Mvc;

// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // Rejestrowanie w konterze wstrzykiwania zale¿noœci

var app = builder.Build();
    

var lambda = () => "Hello from lambda variable";
string LocalFunction() => "Hello from local function";

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/customers", (ICustomerRepository repository) => repository.Get());

app.MapGet("/api/customers/{id:int}", (ICustomerRepository repository, int id) =>
{
    var customer = repository.Get(id);

    if (customer == null)
    {
        return Results.NotFound();
    }
    else
    {        
        return Results.Ok(customer);
    }
});

app.MapGet("/lambda", lambda);
app.MapGet("/function", LocalFunction);

app.MapGet("/hello", () => HelloHandlers.Hello());
app.MapGet("/hello/{name:alpha}", (string name) => HelloHandlers.Hello(name));

app.Run("http://localhost:5000");
