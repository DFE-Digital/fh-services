using Ardalis.GuardClauses;
using AutoMapper;
using FamilyHubs.Referral.Core.Interfaces.Commands;
using FamilyHubs.Referral.Core.Queries;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Core.Commands.UpdateReferral;

public class UpdateReferralCommand : IRequest<long>, IUpdateReferralCommand
{
    public UpdateReferralCommand(long id, ReferralDto referralDto)
    {
        Id = id;
        ReferralDto = referralDto;
    }

    public long Id { get; }
    public ReferralDto ReferralDto { get; }
}

public class UpdateReferralCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateReferralCommand, long>
{
    public async Task<long> Handle(UpdateReferralCommand request, CancellationToken cancellationToken)
    {
        var entity = GetReferral(request);

        await UpdateStatus(entity, request, cancellationToken);
        await UpdateUserAccount(entity, request, cancellationToken);
        await UpdateRecipient(entity, request, cancellationToken);
        await UpdateReferralService(entity, request, cancellationToken);

        entity = GetReferral(request);

        entity = mapper.Map(request.ReferralDto, entity);
        await UpdateUserAccount(entity, request, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private Data.Entities.Referral GetReferral(UpdateReferralCommand request)
    {
        var entity = context.Referrals.GetAll()
            .FirstOrDefault(x => x.Id == request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Referral), request.Id.ToString());
        }

        return entity;
    }

    private async Task UpdateStatus(Data.Entities.Referral entity, UpdateReferralCommand request, CancellationToken cancellationToken)
    {
        if (entity.Status.Id != request.ReferralDto.Status.Id)
        {
            var updatedStatus = await context.Statuses.SingleOrDefaultAsync(x => x.Name == request.ReferralDto.Status.Name, cancellationToken: cancellationToken);

            if (updatedStatus == null)
            {
                throw new NotFoundException(nameof(Status), request.ReferralDto.Status.Name);
            }

            entity.StatusId = updatedStatus.Id;
            entity.Status = updatedStatus;
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task UpdateUserAccount(Data.Entities.Referral entity, UpdateReferralCommand request, CancellationToken cancellationToken)
    {
        if (entity.UserAccount.Id != request.ReferralDto.ReferralUserAccountDto.Id)
        {
            var updatedReferrer = await context.UserAccounts.SingleOrDefaultAsync(x => x.Id == request.ReferralDto.ReferralUserAccountDto.Id, cancellationToken: cancellationToken);

            UpdateUserAccountRole(entity);

            if (updatedReferrer == null)
            {
                
                context.UserAccounts.Add(mapper.Map<UserAccount>(request.ReferralDto.ReferralUserAccountDto));
                entity.UserAccountId = request.ReferralDto.ReferralUserAccountDto.Id;
                await context.SaveChangesAsync(cancellationToken);
                return;
            }

            entity.UserAccount = updatedReferrer;
            await context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            UpdateUserAccountRole(entity);
        }
    }

    private void UpdateUserAccountRole(Data.Entities.Referral entity)
    {
        if (entity.UserAccount != null && entity.UserAccount.UserAccountRoles != null)
        {
            for (int i = 0; i < entity.UserAccount.UserAccountRoles.Count; i++)
            {
                Role? role = context.Roles.SingleOrDefault(x => x.Name == entity.UserAccount.UserAccountRoles[i].Role.Name);
                if (role != null)
                {
                    UserAccountRole? userAccountRole = context.UserAccountRoles.SingleOrDefault(x => x.RoleId == role.Id && x.UserAccountId == entity.UserAccount.Id);
                    if (userAccountRole != null)
                    {
                        entity.UserAccount.UserAccountRoles[i] = userAccountRole;
                        return;
                    }

                    entity.UserAccount.UserAccountRoles[i].Role = role;
                    entity.UserAccount.UserAccountRoles[i].RoleId = role.Id;
                    entity.UserAccount.UserAccountRoles[i].UserAccountId = entity.UserAccount.Id;
                }
            }
        }
    }

    private async Task UpdateRecipient(Data.Entities.Referral entity, UpdateReferralCommand request, CancellationToken cancellationToken)
    {
        if (entity.Recipient.Id != request.ReferralDto.RecipientDto.Id)
        {
            var updatedRecipient = context.Recipients.SingleOrDefault(x => x.Id == request.ReferralDto.RecipientDto.Id);

            if (updatedRecipient == null)
            {
                context.Recipients.Add(mapper.Map<Recipient>(request.ReferralDto.RecipientDto));
                await context.SaveChangesAsync(cancellationToken);
                var recipient = await context.Recipients.SingleOrDefaultAsync(x => x.Email == request.ReferralDto.RecipientDto.Email);
                if (recipient != null)
                {
                    entity.RecipientId = recipient.Id;
                    entity.Recipient = recipient;
                    await context.SaveChangesAsync(cancellationToken);
                }
                return;
            }

            entity.Recipient = updatedRecipient;
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task UpdateReferralService(Data.Entities.Referral entity, UpdateReferralCommand request, CancellationToken cancellationToken)
    {
        if (entity.ReferralService.Id != request.ReferralDto.ReferralServiceDto.Id)
        {
            var updatedReferralService = context.ReferralServices.SingleOrDefault(x => x.Id == request.ReferralDto.ReferralServiceDto.Id);

            if (updatedReferralService == null)
            {

                context.ReferralServices.Add(mapper.Map<Data.Entities.ReferralService>(request.ReferralDto.ReferralServiceDto));
                await context.SaveChangesAsync(cancellationToken);
                return;
            }

            entity.ReferralService = updatedReferralService;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}

