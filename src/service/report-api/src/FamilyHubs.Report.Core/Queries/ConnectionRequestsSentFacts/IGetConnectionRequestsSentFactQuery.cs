using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;

public interface IGetConnectionRequestsSentFactQuery
{
    public Task<ConnectionRequests> GetConnectionRequestsForAdmin(ConnectionRequestsRequest request,
        CancellationToken cancellationToken = default);

    public Task<ConnectionRequests> GetTotalConnectionRequestsForAdmin(CancellationToken cancellationToken = default);

    public Task<ConnectionRequests> GetConnectionRequestsForLa(LaConnectionRequestsRequest request,
        CancellationToken cancellationToken = default);

    public Task<ConnectionRequests> GetTotalConnectionRequestsForLa(LaConnectionRequestsTotalRequest request,
        CancellationToken cancellationToken = default);
}
