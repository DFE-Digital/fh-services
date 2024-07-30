using FamilyHubs.Report.Core.Queries.Common;
using FamilyHubs.Report.Core.Queries.ServiceSearchFacts.Requests;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Reports.WeeklyBreakdown;

namespace FamilyHubs.Report.Core.Queries.ServiceSearchFacts;

public class GetFourWeekBreakdownQuery : IGetFourWeekBreakdownQuery
{
    private readonly IGetServiceSearchFactQuery _getServiceSearchFactQuery;

    public GetFourWeekBreakdownQuery(IGetServiceSearchFactQuery getServiceSearchFactQuery)
    {
        _getServiceSearchFactQuery = getServiceSearchFactQuery;
    }

    public async Task<WeeklyReportBreakdown> GetFourWeekBreakdownForAdmin(SearchBreakdownRequest request, CancellationToken cancellationToken = default) =>
        GetWeeklyReportBreakdown(await GetWeeklyReports(request.Date!.Value, request.ServiceTypeId!.Value, null, cancellationToken));

    public async Task<WeeklyReportBreakdown> GetFourWeekBreakdownForLa(LaSearchBreakdownRequest request, CancellationToken cancellationToken = default) =>
        GetWeeklyReportBreakdown(await GetWeeklyReports(request.Date!.Value, request.ServiceTypeId!.Value, request.LaOrgId!.Value, cancellationToken));

    private async Task<WeeklyReport[]> GetWeeklyReports(DateTime currentDate, ServiceType serviceTypeId, long? laOrgId,
        CancellationToken cancellationToken)
    {
        WeeklyReport[] weeklyReports = new WeeklyReport[4];

        DateTime dateSunday = WeeklyBreakdownCommon.GetEarliestSunday(currentDate);

        for (int i = 0; i < weeklyReports.Length; i++)
        {
            int searchCount = await GetSearchCount(laOrgId, dateSunday, serviceTypeId, 7, cancellationToken);

            weeklyReports[i] = new WeeklyReport
            {
                Date = WeeklyBreakdownCommon.GetMondayToSundayName(dateSunday),
                SearchCount = searchCount
            };

            dateSunday = dateSunday.AddDays(7);
        }

        return weeklyReports;
    }

    private async Task<int> GetSearchCount(long? laOrgId, DateTime date, ServiceType serviceTypeId, int amountOfDays,
        CancellationToken cancellationToken)
        => laOrgId == null
            ? await _getServiceSearchFactQuery.GetSearchCountForAdmin(new SearchCountRequest(date, serviceTypeId, amountOfDays),
                cancellationToken)
            : await _getServiceSearchFactQuery.GetSearchCountForLa(new LaSearchCountRequest(date, serviceTypeId, (long)laOrgId, amountOfDays),
                cancellationToken);

    private static WeeklyReportBreakdown GetWeeklyReportBreakdown(WeeklyReport[] weeklyReports)
        => new()
        {
            WeeklyReports = weeklyReports,
            TotalSearchCount = weeklyReports.Sum(weeklyReport => weeklyReport.SearchCount)
        };
}
