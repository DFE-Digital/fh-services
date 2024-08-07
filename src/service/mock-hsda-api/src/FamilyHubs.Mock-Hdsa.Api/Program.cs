using FamilyHubs.Mock_Hdsa.Api;
using FamilyHubs.Mock_Hdsa.Api.MockResponseGenerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var openApiDoc = builder.Services.AddOpenApiSpecFromFile();
builder.Services.AddEndpointsApiExplorer();

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

// Add configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddDbContext<MockDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("HsdsMockResponsesConnection")));

builder.Services.AddTransient<DbMockResponseGenerator>();
builder.Services.AddTransient<IMockResponseGenerator, HsdaPagingMockResponseGenerator>();

var app = builder.Build();

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
