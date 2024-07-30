using FamilyHubs.Report.Core.Queries.Common;
using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;

namespace FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;

public class GetConnectionRequestsSentFactFourWeekBreakdownQuery : IGetConnectionRequestsSentFactFourWeekBreakdownQuery
{
    private readonly IGetConnectionRequestsSentFactQuery _getConnectionRequestsSentFactQuery;

    public GetConnectionRequestsSentFactFourWeekBreakdownQuery(
        IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQuery)
    {
        _getConnectionRequestsSentFactQuery = getConnectionRequestsSentFactQuery;
    }

    public async Task<ConnectionRequestsBreakdown> GetFourWeekBreakdownForAdmin(
        ConnectionRequestsBreakdownRequest request,
        CancellationToken cancellationToken = default) =>
        GetWeeklyReportBreakdown(await GetWeeklyReports(request.Date!.Value, null, cancellationToken));

    public async Task<ConnectionRequestsBreakdown> GetFourWeekBreakdownForLa(
        LaConnectionRequestsBreakdownRequest request,
        CancellationToken cancellationToken = default) =>
        GetWeeklyReportBreakdown(await GetWeeklyReports(request.Date!.Value, request.LaOrgId!.Value,
            cancellationToken));

    private async Task<ConnectionRequestsDated[]> GetWeeklyReports(DateTime currentDate, long? laOrgId,
        CancellationToken cancellationToken)
    {
        ConnectionRequestsDated[] weeklyReports = new ConnectionRequestsDated[4];

        DateTime dateSunday = WeeklyBreakdownCommon.GetEarliestSunday(currentDate);

        for (int i = 0; i < weeklyReports.Length; i++)
        {
            weeklyReports[i] = await GetConnectionRequests(dateSunday, 7, laOrgId, cancellationToken);

            dateSunday = dateSunday.AddDays(7);
        }

        return weeklyReports;
    }

    private async Task<ConnectionRequestsDated> GetConnectionRequests(DateTime date, int amountOfDays, long? laOrgId,
        CancellationToken cancellationToken)
    {
        ConnectionRequests connectionRequests =
            laOrgId == null
                ? await _getConnectionRequestsSentFactQuery.GetConnectionRequestsForAdmin(
                    new ConnectionRequestsRequest(date, amountOfDays), cancellationToken)
                : await _getConnectionRequestsSentFactQuery.GetConnectionRequestsForLa(
                    new LaConnectionRequestsRequest(laOrgId, date, amountOfDays), cancellationToken);

        return new ConnectionRequestsDated
        {
            Date = WeeklyBreakdownCommon.GetMondayToSundayName(date),
            Made = connectionRequests.Made,
        };
    }

    private static ConnectionRequestsBreakdown GetWeeklyReportBreakdown(ConnectionRequestsDated[] weeklyReports)
        => new()
        {
            WeeklyReports = weeklyReports,
            Totals = new ConnectionRequests
            {
                Made = weeklyReports.Sum(weeklyReport => weeklyReport.Made),
            }
        };
}
