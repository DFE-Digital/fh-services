using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Commands.Add;

public class AddUserSessionCommand : IRequest<string>
{
    public required string Sid { get; set; }
    public required string Email { get; set; }
}

public class AddUserSessionCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<AddUserSessionCommandHandler> logger)
    : IRequestHandler<AddUserSessionCommand, string>
{
    public async Task<string> Handle(AddUserSessionCommand request, CancellationToken cancellationToken)
    {
        var userSession = await dbContext.UserSessions
            .FirstOrDefaultAsync(r => r.Sid.ToLower() == request.Sid.ToLower(), cancellationToken);

        if (userSession is not null)
        {
            throw new AlreadyExistsException($"UserSession for sid:{request.Sid} already exists");
        }

        logger.LogInformation("Saving UserSession {Sid} to DB", request.Sid);

        var entity = new UserSession
        {
            Sid = request.Sid,
            Email = request.Email,
            LastActive = DateTime.UtcNow
        };

        await dbContext.UserSessions.AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return request.Sid;
    }
}