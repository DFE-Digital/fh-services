using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteAllUserSessionsCommand : IRequest
{
    public required string Email { get; set; }
}

public class DeleteAllUserSessionsCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<DeleteAllUserSessionsCommandHandler> logger)
    : IRequestHandler<DeleteAllUserSessionsCommand>
{
    public async Task Handle(DeleteAllUserSessionsCommand request, CancellationToken cancellationToken)
    {
        var entities = await dbContext.UserSessions.Where(r => r.Email == request.Email).ToListAsync(cancellationToken);

        if (!entities.Any())
        {
            logger.LogInformation("No sessions found");
            return;
        }

        dbContext.UserSessions.RemoveRange(entities);

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("UserSessions deleted");
    }
}