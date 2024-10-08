using AutoMapper;
using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Core.Models;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Core.Queries.GetUserSession;

public class GetUserSessionCommand : IRequest<UserSessionDto?>
{
    public string? Email { get; set; }
    public string? Sid { get; set; }
}

public class GetUserSessionCommandHandler(
    ApplicationDbContext dbContext,
    ILogger<GetUserSessionCommandHandler> logger,
    IMapper mapper)
    : IRequestHandler<GetUserSessionCommand, UserSessionDto?>
{
    public async Task<UserSessionDto?> Handle(GetUserSessionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting session Id={Sid}", request.Sid);

        var dbRecord = await GetUserSession(request, cancellationToken);
        return mapper.Map<UserSessionDto>(dbRecord);
    }

    private async Task<UserSession?> GetUserSession(GetUserSessionCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Email))
        {
            return await dbContext.UserSessions.FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);
        }

        if (!string.IsNullOrEmpty(request.Sid))
        {
            return await dbContext.UserSessions.FirstOrDefaultAsync(r => r.Sid == request.Sid, cancellationToken);
        }

        throw new BadRequestException("Get UserSession must query by 'email' or 'sid'");
    }
}

