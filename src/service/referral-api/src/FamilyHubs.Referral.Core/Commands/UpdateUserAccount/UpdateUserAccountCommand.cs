using Ardalis.GuardClauses;
using AutoMapper;
using FamilyHubs.Referral.Core.Interfaces.Commands;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Core.Commands.UpdateUserAccount;

public class UpdateUserAccountCommand : IRequest<bool>, IUpdateUserAccountCommand
{
    public UpdateUserAccountCommand(long userAccountId, UserAccountDto userAccount)
    {
        UserAccountId = userAccountId;
        UserAccount = userAccount;
    }

    public long UserAccountId { get; }

    public UserAccountDto UserAccount { get; }
}

public class UpdateUserAccountCommandHandler(ApplicationDbContext context, IMapper mapper)
    : BaseUserAccountHandler(context), IRequestHandler<UpdateUserAccountCommand, bool>
{
    public async Task<bool> Handle(UpdateUserAccountCommand request, CancellationToken cancellationToken)
    {
        bool result;
        if (_context.Database.IsSqlServer())
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                result = await UpdateAndUpdateUserAccount(request, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        else
        {
            result = await UpdateAndUpdateUserAccount(request, cancellationToken);
        }

        return result;
    }

    private async Task<bool> UpdateAndUpdateUserAccount(UpdateUserAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstOrDefaultAsync(x => x.Id == request.UserAccountId, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Referral), request.UserAccountId.ToString());
        }

        entity = mapper.Map<UserAccount>(request.UserAccount);
        ArgumentNullException.ThrowIfNull(entity);

        entity.OrganisationUserAccounts = mapper.Map<List<UserAccountOrganisation>>(request.UserAccount.OrganisationUserAccounts);

        entity = await AttachExistingUserAccountRoles(entity, cancellationToken);
        entity = await AttachExistingService(entity, cancellationToken);
        entity = await AttachExistingOrgansiation(entity, cancellationToken);

        entity.Name = request.UserAccount.Name;
        entity.PhoneNumber = request.UserAccount.PhoneNumber; 
        entity.EmailAddress = request.UserAccount.EmailAddress;
        entity.Team = request.UserAccount.Team;
        
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
