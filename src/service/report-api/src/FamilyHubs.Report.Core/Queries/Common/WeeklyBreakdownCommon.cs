namespace FamilyHubs.Report.Core.Queries.Common;

public static class WeeklyBreakdownCommon
{
    public static DateTime GetEarliestSunday(DateTime date) => date.AddDays(-(int)date.DayOfWeek).AddDays(-21);

    public static string GetMondayToSundayName(DateTime dateSunday)
    {
        DateTime dateMonday = dateSunday.AddDays(-6);

        // "d MMMM" will convert a date to the day number and full month name.
        // E.g., "08/04/2024" will convert to "8 April"
        return dateMonday.ToString("d MMMM") + " to " + dateSunday.ToString("d MMMM");
    }
}
