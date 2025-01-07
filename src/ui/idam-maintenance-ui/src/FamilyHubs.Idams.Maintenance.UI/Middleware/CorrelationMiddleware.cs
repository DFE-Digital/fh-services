using System.Diagnostics.CodeAnalysis;
using FamilyHubs.SharedKernel.Identity;

namespace FamilyHubs.Idams.Maintenance.UI.Middleware;

[ExcludeFromCodeCoverage]
public class CorrelationMiddleware : IMiddleware
{
    private readonly ILogger<CorrelationMiddleware> _logger;

    public CorrelationMiddleware(ILogger<CorrelationMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = context.Request?.Headers["X-Correlation-ID"].ToString();

        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
        }

        var user = context.GetFamilyHubsUser();
        var userIdentifier = "Anonymous";
        if (!string.IsNullOrEmpty(user.Email))
        {
            userIdentifier = user.Email;
        }

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["UserIdentifier"] = userIdentifier
        }))
        {
            await next(context);
        }
    }
}
