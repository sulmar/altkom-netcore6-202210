using Altkom.Net6.Domain;
using Altkom.Net6.MinimalApi;

var app = WebApplication.Create();

var lambda = () => "Hello from lambda variable";
string LocalFunction() => "Hello from local function";

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/customers", () =>
{
    var customers = new List<Customer>
    {
        new Customer { Id = 1, FirstName = "John", LastName = "Smith", Email="john@domain.com", Gender = Gender.Male, Salary = 1000 },
        new Customer { Id = 2, FirstName = "Kate", LastName = "Smith", Email="kate@domain.com", Gender = Gender.Female, Salary = 2000 },
        new Customer { Id = 3, FirstName = "Bob", LastName = "Smith", Email="bob@domain.com", Gender = Gender.Male, Salary = 3000 },
    };

    return customers;
});


app.MapGet("/api/customers/{id:int}", (int id) => $"Hello Customer Id = {id}!");

app.MapGet("/lambda", lambda);
app.MapGet("/function", LocalFunction);

app.MapGet("/hello", () => HelloHandlers.Hello());
app.MapGet("/hello/{name:alpha}", (string name) => HelloHandlers.Hello(name));

app.Run("http://localhost:5000");
