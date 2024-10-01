using AutoMapper;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Core.Queries.GetReferrals;

public class GetReferralsByReferrerByReferrerIdCommand : IRequest<PaginatedList<ReferralDto>>
{
    public GetReferralsByReferrerByReferrerIdCommand(long id, ReferralOrderBy? orderBy, bool? isAssending, bool? includeDeclined, int? pageNumber, int? pageSize)
    {
        Id = id;
        OrderBy = orderBy;
        IsAssending = isAssending;
        IncludeDeclined = includeDeclined;
        PageNumber = pageNumber ?? 1;
        PageSize = pageSize ?? 10;
    }

    public long Id { get; set; }
    public ReferralOrderBy? OrderBy { get; init; }
    public bool? IsAssending { get; init; }
    public bool? IncludeDeclined { get; init; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetReferralsByReferrerByReferrerIdCommandHandler(ApplicationDbContext context, IMapper mapper)
    : GetReferralsHandlerBase(context, mapper),
        IRequestHandler<GetReferralsByReferrerByReferrerIdCommand, PaginatedList<ReferralDto>>
{
    public async Task<PaginatedList<ReferralDto>> Handle(GetReferralsByReferrerByReferrerIdCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Referrals.GetAll()
            .AsSplitQuery()
            .AsNoTracking();

        if (request.IncludeDeclined != null && request.IncludeDeclined == true)
        {
            entities = entities.Where(x => x.UserAccount.Id == request.Id);
        }
        else
        {
            entities = entities.Where(x => x.UserAccount.Id == request.Id && x.Status.Name != "Declined");
        }

        entities = OrderBy(entities, request.OrderBy, request.IsAssending, true);

        return await GetPaginatedList(request == null, entities, request?.PageNumber ?? 1, request?.PageSize ?? 10);
    }
}

