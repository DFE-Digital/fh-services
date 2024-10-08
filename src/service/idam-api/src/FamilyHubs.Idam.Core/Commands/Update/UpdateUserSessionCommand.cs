using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Update;

public class UpdateUserSessionCommand : IRequest<string>
{
    public required string Sid { get; set; }
}

public class UpdateUserSessionCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<UpdateUserSessionCommandHandler> logger)
    : IRequestHandler<UpdateUserSessionCommand, string>
{
    public async Task<string> Handle(UpdateUserSessionCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.UserSessions
            .FirstOrDefaultAsync(r => r.Sid == request.Sid, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException($"UserSession {request.Sid} not found");
        }

        logger.LogInformation("Updating UserSession {Sid} in DB", request.Sid);

        entity.LastActive = DateTime.UtcNow;
        dbContext.UserSessions.Update(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.Sid;
    }
}