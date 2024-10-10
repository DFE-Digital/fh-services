using System.Diagnostics;
using System.Net;
using AutoMapper;
using FamilyHubs.Referral.Core.ClientServices;
using FamilyHubs.Referral.Core.Interfaces.Commands;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Entities.Metrics;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.SharedKernel.Identity.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Referral.Core.Commands.CreateReferral;

public record CreateReferralCommand(CreateReferralDto CreateReferral, FamilyHubsUser FamilyHubsUser)
    : IRequest<ReferralResponse>, ICreateReferralCommand;

public class CreateReferralCommandHandler(
    ApplicationDbContext context,
    IMapper mapper,
    IServiceDirectoryService serviceDirectoryService)
    : IRequestHandler<CreateReferralCommand, ReferralResponse>
{
    public async Task<ReferralResponse> Handle(CreateReferralCommand request, CancellationToken cancellationToken)
    {
        Data.Entities.Referral entity = mapper.Map<Data.Entities.Referral>(request.CreateReferral.Referral);

        //todo: I don't think these explicit transactions are necessary
        ReferralResponse? referralResponse = null;

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            referralResponse = await CreateAndUpdateReferral(entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await WriteCreateReferralMetrics(request, entity.ReferralService.Organisation.Id, referralResponse);
        }

        return referralResponse;
    }

    private async Task WriteCreateReferralMetrics(CreateReferralCommand request, long vcsOrgId, ReferralResponse? referralResponse)
    {
        var metrics = new ConnectionRequestsSentMetric
        {
            LaOrganisationId = long.Parse(request.FamilyHubsUser.OrganisationId),
            UserAccountId = long.Parse(request.FamilyHubsUser.AccountId),
            VcsOrganisationId = vcsOrgId,
            RequestTimestamp = request.CreateReferral.Metrics.RequestTimestamp.DateTime,
            RequestCorrelationId = Activity.Current!.TraceId.ToString(),
            ResponseTimestamp = referralResponse == null ? null : DateTime.UtcNow,
            HttpResponseCode = referralResponse == null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            ConnectionRequestId = referralResponse?.Id,
            ConnectionRequestReferenceCode = referralResponse?.Id.ToString("X6")
        };

        context.Add(metrics);
        await context.SaveChangesAsync();
    }

    private async Task<ReferralResponse> CreateAndUpdateReferral(Data.Entities.Referral entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.Recipient.Id = 0;

        entity = AttachExistingStatus(entity);
        entity = AttachExistingUserAccount(entity);
        entity = await AttachExistingService(entity);

        context.Referrals.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return new ReferralResponse
        {
            Id = entity.Id,
            ServiceName = entity.ReferralService.Name,
            OrganisationId = entity.ReferralService.Organisation.Id,
        };
    }

    private Data.Entities.Referral AttachExistingStatus(Data.Entities.Referral entity)
    {
        Status? referralStatus = context.Statuses.SingleOrDefault(x => x.Name == entity.Status.Name);
        if (referralStatus != null)
        {
            entity.Status = referralStatus;
        }

        return entity;
    }

    private Data.Entities.Referral AttachExistingUserAccount(Data.Entities.Referral entity)
    {
        UserAccount? professional = context.UserAccounts.SingleOrDefault(x => x.Id == entity.UserAccount.Id);
        if (professional != null)
        {
            entity.UserAccount = professional;
        }
        else
        {
            if (entity.UserAccount != null && entity.UserAccount.UserAccountRoles != null)
            {
                for (int i = 0; i < entity.UserAccount.UserAccountRoles.Count; i++)
                {
                    Role? role =
                        context.Roles.SingleOrDefault(x => x.Name == entity.UserAccount.UserAccountRoles[i].Role.Name);
                    if (role != null)
                    {
                        UserAccountRole? userAccountRole = context.UserAccountRoles.SingleOrDefault(x =>
                            x.RoleId == role.Id && x.UserAccountId == entity.UserAccount.Id);
                        if (userAccountRole != null)
                        {
                            entity.UserAccount.UserAccountRoles[i] = userAccountRole;
                            return entity;
                        }

                        entity.UserAccount.UserAccountRoles[i].Role = role;
                        entity.UserAccount.UserAccountRoles[i].RoleId = role.Id;
                        entity.UserAccount.UserAccountRoles[i].UserAccountId = entity.UserAccount.Id;
                    }
                }
            }
        }

        return entity;
    }

    private async Task<Data.Entities.Referral> AttachExistingService(Data.Entities.Referral entity)
    {
        Data.Entities.ReferralService? referralService =
            context.ReferralServices.SingleOrDefault(x => x.Id == entity.ReferralService.Id);
        if (referralService == null)
        {
            ServiceDirectory.Shared.Dto.ServiceDto? sdService =
                await serviceDirectoryService.GetServiceById(entity.ReferralService.Id);
            if (sdService == null)
            {
                throw new ArgumentException(
                    $"Failed to return Service from service directory for Id = {entity.ReferralService.Id}");
            }

            // check if the organization already exists
            //todo: do we need to update the organisation from the sd, if it already exists?
            Organisation? organisation = await context.Organisations.FindAsync(sdService.OrganisationId);
            if (organisation == null)
            {
                ServiceDirectory.Shared.Dto.OrganisationDto? sdOrganisation =
                    await serviceDirectoryService.GetOrganisationById(sdService.OrganisationId);
                if (sdOrganisation == null)
                {
                    throw new ArgumentException(
                        $"Failed to return Organisation from service directory for Id = {sdService.OrganisationId}");
                }

                //todo: Organisation has a ReferralServiceId, but an organisation can have multiple services
                organisation = new Organisation
                {
                    Id = sdOrganisation.Id,
                    Name = sdOrganisation.Name,
                    Description = sdOrganisation.Description
                };
            }

            Data.Entities.ReferralService srv = new Data.Entities.ReferralService
            {
                Id = sdService.Id,
                Name = sdService.Name,
                Description = sdService.Description,
                OrganizationId = organisation.Id,
                Organisation = organisation
            };

            context.ReferralServices.Add(srv);
            await context.SaveChangesAsync();
            referralService = context.ReferralServices.SingleOrDefault(x => x.Id == entity.ReferralService.Id);
        }

        if (referralService != null)
        {
            entity.ReferralService = referralService;
        }

        return entity;
    }
}
