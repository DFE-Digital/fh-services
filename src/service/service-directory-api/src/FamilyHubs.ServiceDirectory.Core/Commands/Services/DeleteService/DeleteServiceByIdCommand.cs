using Ardalis.GuardClauses;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.ServiceDirectory.Core.Commands.Services.DeleteService;

public class DeleteServiceByIdCommand : IRequest<bool>
{
    public DeleteServiceByIdCommand(long id)
    {
        Id = id;
    }

    public long Id { get; }
}

public class DeleteServiceByIdCommandHandler : IRequestHandler<DeleteServiceByIdCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DeleteServiceByIdCommandHandler> _logger;

    public DeleteServiceByIdCommandHandler(ApplicationDbContext context,
        ILogger<DeleteServiceByIdCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteServiceByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Service? entity = await _context.Services
                .FirstOrDefaultAsync(service => service.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Service), request.Id.ToString());

            entity.Status = ServiceStatusType.Defunct;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred deleting the Service with ID {sId}. {exceptionMessage}", request.Id,
                ex.Message);
            throw;
        }
    }
}