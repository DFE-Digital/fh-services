using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeAndDateDim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     DECLARE @START_DATE AS DATE = '01-01-2023';
                                     IF NOT EXISTS(SELECT * FROM dim.DateDim)
                                     BEGIN
                                         SET IDENTITY_INSERT dim.DateDim ON;
                                         INSERT INTO dim.DateDim (DateKey, Date, DateString, DayNumberOfWeek, DayOfWeekName, DayNumberOfMonth, DayNumberOfYear, WeekNumberOfYear, MonthName, MonthNumberOfYear, CalendarQuarterNumberOfYear, CalendarYearNumber, IsWeekend, IsLeapYear)
                                             SELECT FORMAT(DATEADD(day, value, @START_DATE), 'yyyyMMdd', 'en-GB') DateKey,
                                                DATEADD(day, value, @START_DATE) Date,
                                                FORMAT(DATEADD(day, value, @START_DATE), 'dd/MM/yyyy', 'en-GB') DateString,
                                                DATEPART(dw, DATEADD(day, value - 1, @START_DATE)) DayNumberOfWeek,
                                                DATENAME(dw, DATEADD(day, value, @START_DATE)) DayOfWeekName,
                                                DATEPART(d, DATEADD(day, value, @START_DATE)) DayNumberOfMonth,
                                                DATEPART(dy, DATEADD(day, value, @START_DATE)) DayNumberOfYear,
                                                DATEPART(isowk, DATEADD(day, value, @START_DATE)) WeekNumberOfYear,
                                                DATENAME(m, DATEADD(day, value, @START_DATE)) MonthName,
                                                DATEPART(m, DATEADD(day, value, @START_DATE)) MonthNumberOfYear,
                                                DATEPART(q, DATEADD(day, value, @START_DATE)) CalendarQuarterNumberOfYear,
                                                DATEPART(yyyy, DATEADD(day, value, @START_DATE)) CalendarYearNumber,
                                                IIF(DATEPART(dw, DATEADD(day, value - 1, @START_DATE)) > 5, 'TRUE', 'FALSE') IsWeekend,
                                                IIF(ISDATE(CAST(DATEPART(yyyy, DATEADD(day, value, @START_DATE)) AS char(4)) + '0229') = 1, 'TRUE', 'FALSE') IsLeapYear
                                             FROM GENERATE_SERIES(0, 4747);
                                         SET IDENTITY_INSERT dim.DateDim OFF;
                                     END;
                                 """);

            migrationBuilder.Sql("""
                                     IF NOT EXISTS(SELECT * FROM dim.TimeDim)
                                     BEGIN
                                         INSERT INTO dim.TimeDim (Time, TimeString, HourNumberOfDay, MinuteNumberOfHour, SecondNumberOfMinute)
                                             SELECT DATEADD(second, value, cast('00:00:00' as TIME(0))) Time,
                                                    FORMAT(DATEADD(second, value, cast('00:00:00' as DATETIME)), 'HH:mm:ss') DateString,
                                                    value / 3600 HourNumberOfDay,
                                                    (value / 60) % 60 MinuteNumberOfHour,
                                                    value % 60 SecondNumberOfMinute
                                                 FROM GENERATE_SERIES(0, 86400);
                                     END;
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Method intentionally left empty.
        }
    }
}
