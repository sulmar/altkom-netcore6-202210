using Altkom.Net6.Domain;
using Altkom.Net6.Infrastructure;
using Altkom.Net6.MinimalApi;
using Altkom.Net6.MinimalApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // Rejestrowanie w konterze wstrzykiwania zale¿noœci

var app = builder.Build();


var lambda = () => "Hello from lambda variable";
string LocalFunction() => "Hello from local function";

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/customers", (ICustomerRepository repository) => repository.Get());

// Przyk³ad z u¿yciem funkcji
//app.MapGet("/api/customers/{id:int}", (ICustomerRepository repository, int id) =>
//{
//    var customer = repository.Get(id);

//    if (customer == null)
//    {
//        return Results.NotFound();
//    }
//    else
//    {        
//        return Results.Ok(customer);
//    }
//});

// Przyk³ad z u¿yciem operatora is i ?
//app.MapGet("/api/customers/{id:int}", (ICustomerRepository repository, int id) 
//    => repository.Get(id) is Customer customer ? Results.Ok(customer) : Results.NotFound());

// Przyk³ad z u¿yciem operatora switch i Match Patterns
app.MapGet("/api/customers/{id:int}", (ICustomerRepository repository, int id) => repository.Get(id) switch
 {
     Customer customer => Results.Ok(customer),
     null => Results.NotFound() 
 }).WithName("GetCustomerById");


app.MapGet("/api/customers/{email}", (ICustomerRepository repository, string email) => {
    var customer = repository.GetByEmail(email);

    if (customer == null)
        return Results.NotFound();
    else
        return Results.Ok(customer);
});

app.MapPost("/api/customers", (Customer customer, ICustomerRepository repository) =>
{
    repository.Add(customer);

    // z³a praktyka
    // return Results.Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

    // Dobra praktyka
    return Results.CreatedAtRoute("GetCustomerById", new { customer.Id }, customer);
});

app.MapGet("/api/customers/{id}/link", (int id, LinkGenerator linkGenerator) 
    => $"The link to customer {linkGenerator.GetPathByName("GetCustomerById", values: new { id })}");

app.MapPut("/api/customers/{id}", (int id, Customer customer, ICustomerRepository repository) =>
{
    if (id != customer.Id)
        return Results.BadRequest();

    repository.Update(customer);

    return Results.NoContent();


});

app.MapDelete("/api/customers/{id}", (int id, ICustomerRepository repository) => repository.Remove(id));

// HEAD /api/customers/{id}
//app.MapMethods("/api/customers/{id}", new[] { "HEAD" }, (int id, ICustomerRepository repository) =>
//{
//    if (repository.Exists(id))
//        return Results.Ok();
//    else
//        return Results.NotFound();
//});

// Rekaktoring - u¿ycie metody rozszerzaj¹cej w celu utworzenia metody MapHead
app.MapHead("/api/customers/{id}", (int id, ICustomerRepository repository) =>
{
    if (repository.Exists(id))
        return Results.Ok();
    else
        return Results.NotFound();
});

// JSON Patch (RFC 6902)
// https://jsonpatch.com/
// Content-Type: application/json-patch+json

// dotnet add package Microsoft.AspNetCore.JsonPatch --version 6.0.10
app.MapPatch("/api/customers/{id}", (int id, 
    JsonPatchDocument<Customer> patchCustomer, 
    ICustomerRepository repository) =>
{

});

app.MapGet("/lambda", lambda);
app.MapGet("/function", LocalFunction);

app.MapGet("/hello", () => HelloHandlers.Hello());
app.MapGet("/hello/{name:alpha}", (string name) => HelloHandlers.Hello(name));

app.Run("http://localhost:5000");
