using Altkom.Net6.Domain;
using Altkom.Net6.Infrastructure;
using Altkom.Net6.MinimalApi;
using Altkom.Net6.MinimalApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();

builder.Services.RegisterServices();

string envName =  Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

Console.WriteLine(envName);

// Domyœlnie
// builder.Configuration.AddJsonFile("appsettings.json", optional: false);
// builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
// builder.Configuration.AddEnvironmentVariables();
// builder.Configuration.AddCommandLine(args);

// > SETX ASPNETCORE_ENVIRONMENT Testing

builder.Configuration.AddXmlFile("appsettings.xml", optional: true);
builder.Configuration.AddIniFile("appsettings.ini", optional: true);

string myKey = builder.Configuration["MyKey"];
Console.WriteLine(myKey);

string parameter1 = builder.Configuration["Sekcja1:Parametr1"];
int subparameter1 = int.Parse(builder.Configuration["Sekcja1:Parametr2:Podparametr1"]);

Console.WriteLine(parameter1);
Console.WriteLine(subparameter1);

var app = builder.Build();

app.MapBasicEndpoints();
app.MapCustomerEndpoints();
app.MapProductEndpoints();
app.MapFilesEndpoints();
app.MapHtmlEndpoints();

app.UseStaticFiles();


app.Run("http://localhost:5000");
