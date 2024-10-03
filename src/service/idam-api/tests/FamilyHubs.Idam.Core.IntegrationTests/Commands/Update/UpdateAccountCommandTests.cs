using FamilyHubs.Idam.Core.Commands.Update;
using FamilyHubs.Idam.Data.Entities;
using FluentAssertions;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Update;

public class UpdateAccountCommandTests : DataIntegrationTestBase<UpdateAccountCommandHandler>
{
    [Fact]
    public async Task ThenAnAccountIsUpdated()
    {
        const long accountId = 2;
        const string updatedEmail = "updated@example.com";

        TestDbContext.Accounts.Add(new Account
        {
            Id = accountId,
            Name = "Test User",
            Email = "original@example.com",
            PhoneNumber = "1234567890",
            Status = 0,
            Claims = new List<AccountClaim>()
        });
        await TestDbContext.SaveChangesAsync();

        var command = new UpdateAccountCommand()
        {
            AccountId = accountId,
            Email = updatedEmail
        };
        var commandHandler = new UpdateAccountCommandHandler(
            TestDbContext,
            MockLogger
            );

        //Act
        await commandHandler.Handle(command, new CancellationToken());

        //Assert
        TestDbContext.Accounts.Should().ContainSingle(a => a.Id == accountId && a.Email == updatedEmail);
    }

    [Fact]
    public async Task ThenUpdatingAnAccountThatDoesNotExistThrowsNotFoundException()
    {
        //Arrange
        var command = new UpdateAccountCommand()
        {
            AccountId = -1,
            Email = "notused@example.com"
        };

        var commandHandler = new UpdateAccountCommandHandler(TestDbContext, MockLogger);

        //Act
        var result = async () => await commandHandler.Handle(command, new CancellationToken());

        //Assert
        await result.Should().ThrowAsync<Ardalis.GuardClauses.NotFoundException>();
    }
}
