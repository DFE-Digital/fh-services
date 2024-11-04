using FamilyHubs.Referral.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Core.Queries.GetReferrals;

public class GetReferralCountByServiceIdCommand : IRequest<int>
{
    public long ServiceId { get; }

    public GetReferralCountByServiceIdCommand(long serviceId)
    {
        ServiceId = serviceId;
    }
}

public class GetReferralCountByServiceIdCommandHandler : IRequestHandler<GetReferralCountByServiceIdCommand, int>
{
    private readonly ApplicationDbContext _context;

    public GetReferralCountByServiceIdCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetReferralCountByServiceIdCommand request, CancellationToken cancellationToken)
    {
        IQueryable<Data.Entities.Referral> query = _context.Referrals
            .AsNoTracking()
            .Include(r => r.Status)
            .Where(r => r.Status.Name == "New" || r.Status.Name == "Opened")
            .Where(r => r.StatusId == r.Status.Id)
            .Where(r => r.ReferralServiceId == request.ServiceId);

        return await query.CountAsync(cancellationToken);
    }
}