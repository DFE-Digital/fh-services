using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;
using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.Report.Data.Entities;
using FamilyHubs.Report.Data.Repository;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ReportApi.UnitTests;

public class WhenUsingGetConnectionRequestsSentQuery
{
    private readonly IGetConnectionRequestsSentFactQuery _getConnectionRequestsSentFactQuery;

    public WhenUsingGetConnectionRequestsSentQuery()
    {
        List<DateDim> dateDimList = new()
        {
            new DateDim
            {
                DateKey = 0,
                Date = DateTime.Parse("2024-08-08")
            },
            new DateDim
            {
                DateKey = 1,
                Date = DateTime.Parse("2024-08-04")
            },
            new DateDim
            {
                DateKey = 2,
                Date = DateTime.Parse("2024-06-08")
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
            }
        };

        List<ConnectionRequestsSentFact> connectionRequestsSentFactList = new()
        {
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0],
                VcsOrganisationId = 1
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0],
                VcsOrganisationId = 1
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1],
                VcsOrganisationId = 2
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1],
                VcsOrganisationId = 2
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 1,
                DateDim = dateDimList[1],
                OrganisationDim = organisationDimList[0],
                VcsOrganisationId = 1
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 2,
                DateDim = dateDimList[1],
                OrganisationDim = organisationDimList[1],
                VcsOrganisationId = 3
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[2],
            }
        };

        IReportDbContext reportDbContextMock = Substitute.For<IReportDbContext>();

        reportDbContextMock.ConnectionRequestsSentFacts.Returns(connectionRequestsSentFactList.AsQueryable());

        reportDbContextMock.CountAsync(Arg.Any<IQueryable<ConnectionRequestsSentFact>>(), Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var queryable = callInfo.ArgAt<IQueryable<ConnectionRequestsSentFact>>(0);
                return Task.FromResult(queryable.Count());
            });

        _getConnectionRequestsSentFactQuery = new GetConnectionRequestsSentFactQuery(reportDbContextMock);
    }

    [Theory]
    [InlineData("2024-08-08", 1, 4)]
    [InlineData("2024-08-04", 1, 2)]
    [InlineData("2024-06-08", 1, 1)]
    [InlineData("2024-08-08", 7, 6)]
    [InlineData("2024-08-04", 7, 2)]
    [InlineData("2024-06-08", 7, 1)]
    [InlineData("2024-12-31", 365, 7)]
    public async Task Then_GetConnectionRequestsForAdmin_Should_Return_ExpectedResult(string dateStr, int days, int requestsMade)
    {
        ConnectionRequests expected = new()
        {
            Made = requestsMade
        };

        DateTime dateTime = DateTime.Parse(dateStr);

        ConnectionRequestsRequest request = new(dateTime, days);

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetConnectionRequestsForAdmin(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetTotalConnectionRequestsForAdmin_Should_Return_ExpectedResult()
    {
        ConnectionRequests expected = new()
        {
            Made = 7
        };

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetTotalConnectionRequestsForAdmin();

        Assert.Equivalent(expected, result);
    }

    // orgId 10 == LA
    // orgId 1  == VCS
    [Theory]
    [InlineData(10, "2024-08-08", 1, 2)]
    [InlineData(10, "2024-08-04", 1, 1)]
    [InlineData(10, "2024-06-08", 1, 0)]
    [InlineData(10, "2024-08-08", 7, 3)]
    [InlineData(10, "2024-08-04", 7, 1)]
    [InlineData(10, "2024-06-08", 7, 0)]
    [InlineData(10, "2024-12-31", 365, 3)]
    [InlineData(1, "2024-08-08", 1, 2)]
    [InlineData(1, "2024-08-04", 1, 1)]
    [InlineData(1, "2024-06-08", 1, 0)]
    [InlineData(1, "2024-08-08", 7, 3)]
    [InlineData(1, "2024-08-04", 7, 1)]
    [InlineData(1, "2024-06-08", 7, 0)]
    [InlineData(1, "2024-12-31", 365, 3)]
    [InlineData(100, "2024-12-31", 365, 0)]
    public async Task Then_GetConnectionRequestsForOrg_Should_Return_ExpectedResult(long orgId, string dateStr, int days, int requestsMade)
    {
        ConnectionRequests expected = new()
        {
            Made = requestsMade
        };

        DateTime dateTime = DateTime.Parse(dateStr);

        OrgConnectionRequestsRequest request = new(orgId, dateTime, days);

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetConnectionRequestsForOrg(request);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [InlineData(10, 3)]
    [InlineData(1, 3)]
    [InlineData(20, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    [InlineData(100, 0)]
    public async Task Then_GetTotalConnectionRequestsForOrg_Should_Return_ExpectedResult(long orgId, int requestsMade)
    {
        ConnectionRequests expected = new()
        {
            Made = requestsMade
        };

        OrgConnectionRequestsTotalRequest request = new(orgId);

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetTotalConnectionRequestsForOrg(request);

        Assert.Equivalent(expected, result);
    }
}
