using Altkom.Net6.Domain;
using Altkom.Net6.MinimalApi.Extensions;
using Microsoft.AspNetCore.JsonPatch;

namespace Altkom.Net6.MinimalApi
{

    public static class MapEndpoints
    {
        public static WebApplication MapCustomerEndpoints(this WebApplication app)
        {
            app.MapGet("/api/customers", (ICustomerRepository repository) => repository.Get());

            // Przykład z użyciem funkcji
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

            // Przykład z użyciem operatora is i ?
            //app.MapGet("/api/customers/{id:int}", (ICustomerRepository repository, int id) 
            //    => repository.Get(id) is Customer customer ? Results.Ok(customer) : Results.NotFound());

            // Przykład z użyciem operatora switch i Match Patterns
            app.MapGet("/api/customers/{id:int}", (ICustomerRepository repository, int id) => repository.Get(id) switch
            {
                Customer customer => Results.Ok(customer),
                null => Results.NotFound()
            }).WithName("GetCustomerById");    // Nazywamy endpoint


            app.MapGet("/api/customers/{email}", (ICustomerRepository repository, string email) =>
            {
                var customer = repository.GetByEmail(email);

                if (customer == null)
                    return Results.NotFound();
                else
                    return Results.Ok(customer);
            });

            app.MapPost("/api/customers", (Customer customer, ICustomerRepository repository) =>
            {
                repository.Add(customer);

                // zła praktyka
                // return Results.Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

                // Dobra praktyka
                return Results.CreatedAtRoute("GetCustomerById", new { customer.Id }, customer); // Dodaje do odpowiedzi nagłówek Location: {link}
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

            // Rekaktoring - użycie metody rozszerzającej w celu utworzenia metody MapHead
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
                ICustomerRepository repository) =>
            {
                // TODO: rozwiązać problem deserializacji
                JsonPatchDocument patchDocument = new JsonPatchDocument();
                patchDocument.Replace("Salary", 1500);

                var customer = repository.Get(id);
                patchDocument.ApplyTo(customer);

                throw new NotImplementedException();
            });


            return app;
        }

        public static WebApplication MapProductEndpoints(this WebApplication app)
        {


            app.MapGet("/api/products", (IProductRepository repository) => repository.Get());
            app.MapGet("/api/products/{id:int}", (IProductRepository repository, int id) => repository.Get(id));
            app.MapGet("/api/products/{code:alpha:length(3)}", (IProductRepository repository, string code) => repository.GetByCode(code));
            app.MapGet("/api/products/{color:alpha}", (IProductRepository repository, string color) => repository.GetByColor(color));

            return app;
        }

        public static WebApplication MapBasicEndpoints(this WebApplication app)
        {
            var lambda = () => "Hello from lambda variable";
            string LocalFunction() => "Hello from local function";

            app.MapGet("/", () => "Hello World!");


            app.MapGet("/lambda", lambda);
            app.MapGet("/function", LocalFunction);

            app.MapGet("/hello", () => HelloHandlers.Hello());
            app.MapGet("/hello/{name:alpha}", (string name) => HelloHandlers.Hello(name));

            return app;
        }
    }
}
