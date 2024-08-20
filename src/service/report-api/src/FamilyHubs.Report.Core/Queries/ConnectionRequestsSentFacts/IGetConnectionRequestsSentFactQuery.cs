using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;

public interface IGetConnectionRequestsSentFactQuery
{
    public Task<ConnectionRequests> GetConnectionRequestsForAdmin(ConnectionRequestsRequest request,
        CancellationToken cancellationToken = default);

    public Task<ConnectionRequests> GetTotalConnectionRequestsForAdmin(CancellationToken cancellationToken = default);

    public Task<ConnectionRequests> GetConnectionRequestsForOrg(OrgConnectionRequestsRequest request,
        CancellationToken cancellationToken = default);

    public Task<ConnectionRequests> GetTotalConnectionRequestsForOrg(OrgConnectionRequestsTotalRequest request,
        CancellationToken cancellationToken = default);
}
