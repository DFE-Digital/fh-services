using Ardalis.GuardClauses;
using FamilyHubs.Referral.Core.Interfaces.Commands;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.SharedKernel.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Core.Commands.SetReferralStatus;

public class SetReferralStatusCommand: IRequest<string>, ISetReferralStatusCommand
{
    public SetReferralStatusCommand(string role, long userOrganisationId, long referralId, string status, string reasonForDecliningSupport)
    {
        Status = status;
        ReferralId = referralId;
        ReasonForDecliningSupport = reasonForDecliningSupport;
        Role = role;
        UserOrganisationId = userOrganisationId;
    }
    public string Role { get; }

    public long UserOrganisationId { get; }
    public long ReferralId { get; }

    public string Status { get; }

    public string ReasonForDecliningSupport { get; }

}

public class SetReferralStatusCommandHandler(ApplicationDbContext context)
    : IRequestHandler<SetReferralStatusCommand, string>
{
    public static string Forbidden => "Forbidden";

    public async Task<string> Handle(SetReferralStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Referrals
        .Include(x => x.Status)
        .Include(x => x.UserAccount)
        .Include(x => x.Recipient)
        .Include(x => x.ReferralService)
        .ThenInclude(x => x.Organisation)
        .FirstOrDefaultAsync(p => p.Id == request.ReferralId, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Referral), request.ReferralId.ToString());
        }

        //Only modify Status if DfEAdmin or belong to VCS Organisation,
        //assumption is VCS Professional will have correct organisation id other users will not
        if (entity.ReferralService.Organisation.Id == request.UserOrganisationId || RoleTypes.DfeAdmin == request.Role) 
        {
            var updatedStatus = await context.Statuses.SingleOrDefaultAsync(x => x.Name == request.Status) ?? throw new NotFoundException(nameof(Status), request.Status);

            entity.ReasonForDecliningSupport = request.ReasonForDecliningSupport;
            entity.StatusId = updatedStatus.Id;
            entity.Status = updatedStatus;
            await context.SaveChangesAsync(cancellationToken);

            return entity.Status.Name;
        }

        return Forbidden;
    }
}
