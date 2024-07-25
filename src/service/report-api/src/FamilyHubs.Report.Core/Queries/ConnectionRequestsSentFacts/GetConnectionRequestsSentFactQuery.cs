using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.Report.Data.Entities;
using FamilyHubs.Report.Data.Repository;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;

public class GetConnectionRequestsSentFactQuery : IGetConnectionRequestsSentFactQuery
{
    private readonly IReportDbContext _reportDbContext;

    public GetConnectionRequestsSentFactQuery(IReportDbContext reportDbContext)
    {
        _reportDbContext = reportDbContext;
    }

    public async Task<ConnectionRequests> GetConnectionRequestsForAdmin(ConnectionRequestsRequest request,
        CancellationToken cancellationToken = default)
    {
        DateTime startDate = request.Date!.Value.AddDays(-request.AmountOfDays!.Value);

        IQueryable<ConnectionRequestsSentFact> query = _reportDbContext.ConnectionRequestsSentFacts
            .Include(cRSf => cRSf.DateDim)
            .Where(cRSf => cRSf.DateDim.Date > startDate && cRSf.DateDim.Date <= request.Date!.Value);

        return new ConnectionRequests
        {
            Made = await _reportDbContext.CountAsync(query, cancellationToken)
        };
    }

    public async Task<ConnectionRequests> GetTotalConnectionRequestsForAdmin(
        CancellationToken cancellationToken = default)
        => new()
        {
            Made = await _reportDbContext.CountAsync(_reportDbContext.ConnectionRequestsSentFacts, cancellationToken)
        };

    public async Task<ConnectionRequests> GetConnectionRequestsForOrg(OrgConnectionRequestsRequest request,
        CancellationToken cancellationToken = default)
    {
        DateTime startDate = request.Date!.Value.AddDays(-request.AmountOfDays!.Value);

        IQueryable<ConnectionRequestsSentFact> query = _reportDbContext.ConnectionRequestsSentFacts
            .Include(cRSf => cRSf.DateDim)
            .Include(cRSf => cRSf.OrganisationDim)
            .Where(cRSf => cRSf.OrganisationDim != null)
            .Where(cRSf => cRSf.DateDim.Date > startDate && cRSf.DateDim.Date <= request.Date!.Value)
            .Where(cRSf => cRSf.OrganisationDim!.OrganisationId == request.OrgId!.Value ||
                           cRSf.VcsOrganisationId == request.OrgId!.Value);

        return new ConnectionRequests
        {
            Made = await _reportDbContext.CountAsync(query, cancellationToken)
        };
    }

    public async Task<ConnectionRequests> GetTotalConnectionRequestsForOrg(OrgConnectionRequestsTotalRequest request,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ConnectionRequestsSentFact> query = _reportDbContext.ConnectionRequestsSentFacts
            .Include(cRSf => cRSf.OrganisationDim)
            .Where(cRSf => cRSf.OrganisationDim != null)
            .Where(cRSf => cRSf.OrganisationDim!.OrganisationId == request.OrgId!.Value ||
                           cRSf.VcsOrganisationId == request.OrgId!.Value);

        return new ConnectionRequests
        {
            Made = await _reportDbContext.CountAsync(query, cancellationToken)
        };
    }
}
