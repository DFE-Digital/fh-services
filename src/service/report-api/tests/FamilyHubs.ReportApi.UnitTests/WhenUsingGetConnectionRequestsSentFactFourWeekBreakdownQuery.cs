using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;
using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.Report.Data.Entities;
using FamilyHubs.Report.Data.Repository;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ReportApi.UnitTests;

public class WhenUsingGetConnectionRequestsSentFactFourWeekBreakdownQuery
{
    private readonly IGetConnectionRequestsSentFactFourWeekBreakdownQuery
        _getConnectionRequestsSentFactFourWeekBreakdownQuery;

    public WhenUsingGetConnectionRequestsSentFactFourWeekBreakdownQuery()
    {
        List<DateDim> dateDimList = new()
        {
            new DateDim
            {
                DateKey = 1,
                Date = DateTime.Parse("2024-01-01") // Week 1
            },
            new DateDim
            {
                DateKey = 2,
                Date = DateTime.Parse("2024-01-08") // Week 2
            },
            new DateDim
            {
                DateKey = 3,
                Date = DateTime.Parse("2024-01-15") // Week 3
            },
            new DateDim
            {
                DateKey = 4,
                Date = DateTime.Parse("2024-01-22") // Week 4
            }
        };

        List<OrganisationDim> organisationDimList = new()
        {
            new OrganisationDim
            {
                OrganisationKey = 1,
                OrganisationId = 10
            },
            new OrganisationDim
            {
                OrganisationKey = 2,
                OrganisationId = 20
            },
            new OrganisationDim
            {
                OrganisationKey = 3,
                OrganisationId = 30
            },
            new OrganisationDim
            {
                OrganisationKey = 4,
                OrganisationId = 40
            },
        };

        List<ConnectionRequestsSentFact> connectionRequestsSentFactList = new()
        {
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0],
                VcsOrganisationKey = 4,
                VcsOrganisationDim = organisationDimList[3]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0],
                VcsOrganisationKey = 4,
                VcsOrganisationDim = organisationDimList[3]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1],
                VcsOrganisationKey = 3,
                VcsOrganisationDim = organisationDimList[2]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1],
                VcsOrganisationKey = 3,
                VcsOrganisationDim = organisationDimList[2]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 3,
                OrganisationKey = 1,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[0],
                VcsOrganisationKey = 4,
                VcsOrganisationDim = organisationDimList[3]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 3,
                OrganisationKey = 2,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[1],
                VcsOrganisationKey = 4,
                VcsOrganisationDim = organisationDimList[3]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 4,
                DateDim = dateDimList[3]
            },
        };

        IReportDbContext reportDbContextMock = Substitute.For<IReportDbContext>();

        reportDbContextMock.ConnectionRequestsSentFacts.Returns(connectionRequestsSentFactList.AsQueryable());

        reportDbContextMock.CountAsync(Arg.Any<IQueryable<ConnectionRequestsSentFact>>(), Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var queryable = callInfo.ArgAt<IQueryable<ConnectionRequestsSentFact>>(0);
                return Task.FromResult(queryable.Count());
            });

        IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQueryMock
            = Substitute.For<GetConnectionRequestsSentFactQuery>(reportDbContextMock);

        _getConnectionRequestsSentFactFourWeekBreakdownQuery =
            new GetConnectionRequestsSentFactFourWeekBreakdownQuery(getConnectionRequestsSentFactQueryMock);
    }

    [Fact]
    public async Task Then_GetFourWeekBreakdownForAdmin_Should_Return_Results_IfRequestsMade()
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 10
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "1 January to 7 January",
                    Made = 4
                },
                new()
                {
                    Date = "8 January to 14 January",
                    Made = 3
                },
                new()
                {
                    Date = "15 January to 21 January",
                    Made = 2
                },
                new()
                {
                    Date = "22 January to 28 January",
                    Made = 1
                },
            }
        };

        ConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2024-01-31"));

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForAdmin(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetFourWeekBreakdownForAdmin_Should_Return_Zero_If_NoRequestsMade()
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 0
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "13 November to 19 November",
                    Made = 0
                },
                new()
                {
                    Date = "20 November to 26 November",
                    Made = 0
                },
                new()
                {
                    Date = "27 November to 3 December",
                    Made = 0
                },
                new()
                {
                    Date = "4 December to 10 December",
                    Made = 0
                },
            }
        };

        ConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2023-12-16"));

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForAdmin(request);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [InlineData(10, 3, 2, 0, 1, 0)] // LA
    [InlineData(30, 2, 2, 0, 0, 0)] // VCS
    public async Task Then_GetFourWeekBreakdownForOrg_Should_Return_Results_IfRequestsMade(
        long orgId,
        int madeTotal, int madeWeekOne, int madeWeekTwo, int madeWeekThree, int madeWeekFour)
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = madeTotal
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "1 January to 7 January",
                    Made = madeWeekOne
                },
                new()
                {
                    Date = "8 January to 14 January",
                    Made = madeWeekTwo
                },
                new()
                {
                    Date = "15 January to 21 January",
                    Made = madeWeekThree
                },
                new()
                {
                    Date = "22 January to 28 January",
                    Made = madeWeekFour
                },
            }
        };

        OrgConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2024-01-31"), orgId);

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForOrg(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetFourWeekBreakdownForOrg_Should_Return_Zero_If_NoRequestsMade()
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 0
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "13 November to 19 November",
                    Made = 0
                },
                new()
                {
                    Date = "20 November to 26 November",
                    Made = 0
                },
                new()
                {
                    Date = "27 November to 3 December",
                    Made = 0
                },
                new()
                {
                    Date = "4 December to 10 December",
                    Made = 0
                },
            }
        };

        OrgConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2023-12-16"), 10);

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForOrg(request);

        Assert.Equivalent(expected, result);
    }
}
