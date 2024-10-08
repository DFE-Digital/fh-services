using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Delete;

public class DeleteUserSessionCommand : IRequest<bool>
{
    public required string Sid { get; set; }
}

public class DeleteUserSessionCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<DeleteUserSessionCommandHandler> logger)
    : IRequestHandler<DeleteUserSessionCommand, bool>
{
    public async Task<bool> Handle(DeleteUserSessionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting session for Sid: {Sid}", request.Sid);

        var entity = await dbContext.UserSessions.FirstOrDefaultAsync(r => r.Sid == request.Sid, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException($"No session found for Sid: {request.Sid}");
        }

        dbContext.UserSessions.Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}