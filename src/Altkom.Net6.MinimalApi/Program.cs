using Altkom.Net6.Domain;
using Altkom.Net6.Infrastructure;
using Altkom.Net6.MinimalApi;
using Altkom.Net6.MinimalApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text;

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


string parameter1 = builder.Configuration["Sekcja1:Parametr1"];
int subparameter1 = int.Parse(builder.Configuration["Sekcja1:Parametr2:Podparametr1"]);

// domyslnie
// builder.Logging.AddConsole();

// dotnet add package Serilog.AspNetCore

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File(new CompactJsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// builder.Logging.AddSerilog(logger);

// dotnet add package OpenTelemetry.Instrumentation.AspNetCore --prelease
// dotnet add package OpenTelemetry.Instrumentation.Http --prelease
// dotnet add package OpenTelemetry.Extensions.Hosting --prelease
// dotnet add package OpenTelemetry.Exporter.Console --prelease
// dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol --prelease

builder.Services.AddOpenTelemetryTracing(builder =>
{
    builder
        .AddConsoleExporter()
        .AddOtlpExporter(options => options.Endpoint = new Uri("http://localhost:4317"))
        .AddSource("MyMinimalApi")
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: "MyMinimalApi", serviceVersion: "1.0"))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation();
});


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

app.Logger.LogTrace(myKey);
app.Logger.LogTrace(parameter1);
app.Logger.LogTrace($"{subparameter1}");

app.Logger.LogInformation("The application started!");

app.UseAuthentication();
app.UseAuthorization();

app.MapBasicEndpoints();
app.MapCustomerEndpoints();
app.MapProductEndpoints();
app.MapFilesEndpoints();
app.MapHtmlEndpoints();

app.UseStaticFiles();


app.Run("http://localhost:5000");
