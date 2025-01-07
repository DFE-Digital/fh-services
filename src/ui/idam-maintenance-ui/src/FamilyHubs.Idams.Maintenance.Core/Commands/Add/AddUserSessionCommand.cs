using FamilyHubs.Idams.Maintenance.Core.Exceptions;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.Commands.Add;

public class AddUserSessionCommand : IRequest<string>
{
    public required string Sid { get; set; }
    public required string Email { get; set; }
}

public class AddUserSessionCommandHandler : IRequestHandler<AddUserSessionCommand, string>
{
    private readonly IRepository _repository;
    private readonly ILogger<AddUserSessionCommandHandler> _logger;

    public AddUserSessionCommandHandler(
        IRepository repository,
        ILogger<AddUserSessionCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<string> Handle(AddUserSessionCommand request, CancellationToken cancellationToken)
    {
        var userSession = await _repository.UserSessions
            .FirstOrDefaultAsync(r => r.Sid.ToLower() == request.Sid.ToLower(), cancellationToken);

        if (userSession is not null)
        {
            var msg = $"UserSession for sid:{request.Sid} already exists";
            _logger.LogError(msg);
            throw new AlreadyExistsException(msg);
        }

        try
        {
            var entity = new UserSession
            {
                Sid = request.Sid,
                Email = request.Email,
                LastActive = DateTime.UtcNow
            };

            await _repository.AddAsync(entity, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UserSession {Sid} saved to DB", request.Sid);

            return request.Sid;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating UserSession for Sid:{Sid}", request.Sid);
            throw;
        }
    }
}