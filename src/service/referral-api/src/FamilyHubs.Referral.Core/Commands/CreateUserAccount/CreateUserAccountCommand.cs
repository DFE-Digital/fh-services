using AutoMapper;
using FamilyHubs.Referral.Core.Interfaces.Commands;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Core.Commands.CreateUserAccount;

public class CreateUserAccountCommand : IRequest<long>, ICreateUserAccountCommand
{
    public CreateUserAccountCommand(UserAccountDto userAccount)
    {
        UserAccount = userAccount;
    }

    public UserAccountDto UserAccount { get; }
}

public class CreateUserAccountCommandHandler(ApplicationDbContext context, IMapper mapper)
    : BaseUserAccountHandler(context), IRequestHandler<CreateUserAccountCommand, long>
{
    public async Task<long> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
    {
        long result;
        if (_context.Database.IsSqlServer())
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                result = await CreateAndUpdateUserAccount(request, cancellationToken);
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
            result = await CreateAndUpdateUserAccount(request, cancellationToken);
        }

        return result;
    }

    private async Task<long> CreateAndUpdateUserAccount(CreateUserAccountCommand request, CancellationToken cancellationToken)
    {
        UserAccount entity = mapper.Map<UserAccount>(request.UserAccount);
        ArgumentNullException.ThrowIfNull(entity);

        entity.OrganisationUserAccounts = mapper.Map<List<UserAccountOrganisation>>(request.UserAccount.OrganisationUserAccounts);

        entity = await AttachExistingUserAccountRoles(entity, cancellationToken);
        entity = await AttachExistingService(entity, cancellationToken);
        entity = await AttachExistingOrgansiation(entity, cancellationToken);

        _context.UserAccounts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}


