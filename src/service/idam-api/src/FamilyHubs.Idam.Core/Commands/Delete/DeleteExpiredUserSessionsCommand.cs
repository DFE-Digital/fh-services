using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteExpiredUserSessionsCommand : IRequest<bool>;

public class DeleteExpiredUserSessionsCommandHandler : IRequestHandler<DeleteExpiredUserSessionsCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteExpiredUserSessionsCommandHandler> _logger;
    private readonly IConfiguration _configuration;

    public DeleteExpiredUserSessionsCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteExpiredUserSessionsCommandHandler> logger, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<bool> Handle(DeleteExpiredUserSessionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeleteExpiredUserSessionsCommand started");

            var cleanupInterval = _configuration.GetValue<int?>("ExpiredSessionCleanupInterval");
            if (cleanupInterval is null)
            {
                _logger.LogError("ExpiredSessionCleanupInterval not configured.");
                throw new IdamsException("ExpiredSessionCleanupInterval not configured.");
            }
            var sessionExpiryTime = DateTime.UtcNow.AddSeconds((int)-cleanupInterval);
            var entities =
                await _dbContext.UserSessions.Where(r => r.LastActive < sessionExpiryTime).ToListAsync(cancellationToken);

            _logger.LogInformation("Found: {CountExpired} expired sessions.", entities.Count);

            if (entities.Count > 0)
            {
                _dbContext.UserSessions.RemoveRange(entities);

                await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Expired UserSessions deleted");
            }

            _logger.LogInformation("DeleteExpiredUserSessionsCommand finished");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred deleting Expired UserSessions");
            return false;
        }
    }
}
