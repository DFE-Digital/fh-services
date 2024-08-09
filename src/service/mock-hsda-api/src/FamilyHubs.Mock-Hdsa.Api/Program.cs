using FamilyHubs.Mock_Hdsa.Api;
using FamilyHubs.Mock_Hdsa.Api.MockResponseGenerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

// custom mock server vs APIM

// devs can run the server locally
// no need for devs & testers to learn APIM
// APIM mock profile is very basic and doesn't support scenarios - still possible by combining other profiles, but working against the tool, rather than the tool working for us
// custom is more flexible - we can easily get it to do as we need as requirements arise. e.g. paging has been implemented without setting up individual page responses. if we wanted to e.g. see if our code handles 1000 pages, we can implement that easily with a little bit of code, rather than manually setting up 1000 different responses in APIM
// automated tests can set up and tear down test responses

var builder = WebApplication.CreateBuilder(args);

var openApiDoc = builder.Services.AddOpenApiSpecFromFile();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = openApiDoc.Info.Title,
        Version = openApiDoc.Info.Version,
        Description = openApiDoc.Info.Description
    });

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
