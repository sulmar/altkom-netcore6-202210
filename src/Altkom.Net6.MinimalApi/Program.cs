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

var app = builder.Build();

app.MapBasicEndpoints();
app.MapCustomerEndpoints();
app.MapProductEndpoints();
app.MapFilesEndpoints();
app.MapHtmlEndpoints();

app.UseStaticFiles();

app.Run("http://localhost:5000");
