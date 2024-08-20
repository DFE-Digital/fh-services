using FamilyHubs.SharedKernel.Identity;

namespace FamilyHubs.Idam.Api.Middleware
{
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
            var userEmail = "Anonymous";
            if (!string.IsNullOrEmpty(user.Email))
            {
                userEmail = ObfuscateEmail(user.Email);
            }

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId,
                ["UserIdentifier"] = userEmail,
                ["AccountId"] = user.AccountId,
                ["UserRole"] = user.Role,
                ["UserOrganisationId"] = user.OrganisationId,
                ["CorrelationTime"] = DateTime.UtcNow.Ticks
            }))
            {
                await next(context);
            }
        }

        public static string ObfuscateEmail(string email)
        {
            int atIndex = email.IndexOf('@');
            if (atIndex < 0)
            {
                return "InvalidEmail";
            }

            // Extract the first two characters and everything after the '@' symbol
            string firstTwoChars = email.Substring(0, Math.Min(2, atIndex));
            string afterAt = email.Substring(atIndex);

            return $"{firstTwoChars}***{afterAt}";
        }
    }
}
