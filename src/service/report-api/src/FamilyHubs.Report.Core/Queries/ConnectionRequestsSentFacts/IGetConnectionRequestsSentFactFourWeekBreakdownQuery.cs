using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;

public interface IGetConnectionRequestsSentFactFourWeekBreakdownQuery
{
    public Task<ConnectionRequestsBreakdown> GetFourWeekBreakdownForAdmin(ConnectionRequestsBreakdownRequest request,
        CancellationToken cancellationToken = default);

    public Task<ConnectionRequestsBreakdown> GetFourWeekBreakdownForLa(LaConnectionRequestsBreakdownRequest request,
        CancellationToken cancellationToken = default);
}
