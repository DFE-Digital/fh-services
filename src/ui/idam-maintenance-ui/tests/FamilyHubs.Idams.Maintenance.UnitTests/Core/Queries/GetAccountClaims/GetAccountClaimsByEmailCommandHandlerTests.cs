using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccountClaims;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Queries.GetAccountClaims;

public class GetAccountClaimsByEmailCommandHandlerTests
{
    [Fact]
    public async Task UnknownAccountId_Handle_ReturnsEmptyAccountClaims()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<GetAccountClaimsByEmailCommandHandler>>();
        var handler = new GetAccountClaimsByEmailCommandHandler(repository, logger);
        var command = new GetAccountClaimsByIdCommand { AccountId = 1000 };
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task KnownAccountId_Handle_ReturnsEmptyAccountClaims()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var account1 = TestAccounts.GetAccount1();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<GetAccountClaimsByEmailCommandHandler>>();
        var handler = new GetAccountClaimsByEmailCommandHandler(repository, logger);
        var command = new GetAccountClaimsByIdCommand { AccountId = account1.Id };
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Should().HaveCount(8);
        result.Find(c => c is { Name: "OrganisationId", Value: "100" }).Should().NotBeNull();
        result.Find(c => c is { Name: "role", Value: "DfeAdmin" }).Should().NotBeNull();
        result.Find(c => c is { Name: "Name", Value: "Jane Doe" }).Should().NotBeNull();
        result.Find(c => c is { Name: "PhoneNumber", Value: "" }).Should().NotBeNull();
        result.Find(c => c is { Name: "AccountStatus", Value: "Active" }).Should().NotBeNull();
        result.Find(c => c is { Name: "Email", Value: "jd@temp.org" }).Should().NotBeNull();
        result.Find(c => c is { Name: "OpenId", Value: "" }).Should().NotBeNull();
        result.Find(c => c is { Name: "AccountId", Value: "1" }).Should().NotBeNull();
    }
}