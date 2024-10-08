using Ardalis.GuardClauses;
using AutoMapper;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Core.Queries.GetReferrals;

public class GetReferralsByOrganisationIdCommand : IRequest<PaginatedList<ReferralDto>>
{
    public GetReferralsByOrganisationIdCommand(long organisationId, ReferralOrderBy? orderBy, bool? isAssending, bool? includeDeclined, int? pageNumber, int? pageSize)
    {
        OrganisationId = organisationId;
        OrderBy = orderBy;
        IsAssending = isAssending;
        PageNumber = pageNumber ?? 1;
        PageSize = pageSize ?? 10;
        IncludeDeclined = includeDeclined;
    }

    public long OrganisationId { get; set; }
    public ReferralOrderBy? OrderBy { get; init; }
    public bool? IsAssending { get; init; }
    public bool? IncludeDeclined { get; init; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetReferralsByOrganisationIdCommandHandler(ApplicationDbContext context, IMapper mapper)
    : GetReferralsHandlerBase(context, mapper),
        IRequestHandler<GetReferralsByOrganisationIdCommand, PaginatedList<ReferralDto>>
{
    public async Task<PaginatedList<ReferralDto>> Handle(GetReferralsByOrganisationIdCommand request, CancellationToken cancellationToken)
    {
        var entities = _context.Referrals.GetAll()
            .AsNoTracking()
            .Where(x => x.ReferralService.Organisation.Id == request.OrganisationId);

        if (request.IncludeDeclined is true)
        {
            entities = entities.Where(x => x.ReferralService.Organisation.Id == request.OrganisationId);
        }
        else
        {
            entities = entities.Where(x => x.ReferralService.Organisation.Id == request.OrganisationId && x.Status.Name != "Declined");
        }

        entities = OrderBy(entities, request.OrderBy, request.IsAssending);

        return await GetPaginatedList(request == null, entities, request?.PageNumber ?? 1, request?.PageSize ?? 10);
    }
}
