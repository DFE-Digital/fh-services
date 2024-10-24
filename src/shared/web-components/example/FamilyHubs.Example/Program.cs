using FamilyHubs.SharedKernel.Health;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services
    .AddFamilyHubs(builder.Configuration)
    .AddFamilyHubsHealthChecks(builder.Configuration);

var app = builder.Build();

app.UseFamilyHubs();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapFamilyHubsHealthChecks();

app.Run();
