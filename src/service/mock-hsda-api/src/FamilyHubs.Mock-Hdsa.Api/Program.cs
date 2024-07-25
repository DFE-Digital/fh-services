using FamilyHubs.Mock_Hdsa.Api;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//await builder.Services.AddOpenApiSpec("https://raw.githubusercontent.com/openreferral/specification/3.0/schema/openapi.json");
var openApiDoc = builder.Services.AddOpenApiSpecFromFile();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HSDA Mock API", Version = "v1" });

    c.CustomOperationIds(apiDesc =>
    {
        var path = apiDesc.RelativePath;
        var method = apiDesc.HttpMethod.ToLowerInvariant();
        return $"{method}_{path?.Replace("/", "_")}";
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//app.UseSwagger();
//app.UseSwaggerUI();
//}

app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Paths = openApiDoc.Paths;
        swaggerDoc.Components = openApiDoc.Components;
    });
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HSDA Mock API V1");
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapFallback(RequestHandler.Handle);
});

app.Run();


