using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectory.Core.Queries.Services.GetServicesByOrganisationId;

//todo: move somewhere common for reuse?
public enum SortOrder
{
    ascending,
    descending
}

//todo: rename ServiceNameDto to ServiceSummaryDto?
public class GetServiceNamesCommand : IRequest<PaginatedList<ServiceNameDto>>
{
    public long? OrganisationId { get; set; }
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
    public required SortOrder Order { get; set; }
    public string? ServiceNameSearch { get; set; }
}

public class GetServiceNamesCommandHandler : IRequestHandler<GetServiceNamesCommand, PaginatedList<ServiceNameDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetServiceNamesCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //todo: throw a NotFoundException if the supplied organisation isn't in the database?
    public async Task<PaginatedList<ServiceNameDto>> Handle(GetServiceNamesCommand request, CancellationToken cancellationToken)
    {
        var services = await GetServiceNames(request, cancellationToken);

        int totalCount = await GetServicesCount(request, cancellationToken);

        return new PaginatedList<ServiceNameDto>(services, totalCount, request.PageNumber, request.PageSize);
    }

    private async Task<List<ServiceNameDto>> GetServiceNames(GetServiceNamesCommand request, CancellationToken cancellationToken)
    {
        int skip = (request.PageNumber - 1) * request.PageSize;

        //todo: do we need _context.ServiceNames?
        var servicesQuery = GetBaseQuery(request);

        servicesQuery = request.Order == SortOrder.ascending
            ? servicesQuery.OrderBy(s => s.Name)
            : servicesQuery.OrderByDescending(s => s.Name);

        return await servicesQuery
            .Skip(skip)
            .Take(request.PageSize)
            .ProjectTo<ServiceNameDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    private async Task<int> GetServicesCount(GetServiceNamesCommand request, CancellationToken cancellationToken)
    {
        var serviceCountQuery = GetBaseQuery(request);

        return await serviceCountQuery.CountAsync(cancellationToken);
    }

    private IQueryable<Service> GetBaseQuery(GetServiceNamesCommand cmd)
    {
        var query = _context.Services
            .Where(s => s.Status != ServiceStatusType.Defunct);

        if (cmd.OrganisationId != null)
        {
            query = query.Where(s => s.OrganisationId == cmd.OrganisationId);
        }
        if (!string.IsNullOrEmpty(cmd.ServiceNameSearch))
        {
            query = query.Where(s => s.Name.Contains(cmd.ServiceNameSearch));
        }

        return query;
    }
}
