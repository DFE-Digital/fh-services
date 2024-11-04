using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace FamilyHubs.SharedKernel.Health;

public class VersionInfoMiddleware
{
    private readonly string _version;

    public VersionInfoMiddleware(
        RequestDelegate next,
        Assembly assembly)
    {
        _version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion!;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        // Get results
        var result = $"Version: {_version}";
        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsync(result);
    }
}
