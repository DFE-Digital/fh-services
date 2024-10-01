using AutoFixture;
using FamilyHubs.Idam.Core.Commands.Add;
using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;
using FluentAssertions;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Add;

public class AddAccountCommandTests : DataIntegrationTestBase<AddAccountCommandHandler>
{
    [Fact]
    public async Task Handle_AccountIsCreated()
    {
        //Arrange
        var command = Fixture.Create<AddAccountCommand>();
        var commandHandler = new AddAccountCommandHandler(TestDbContext, MockLogger.Object);

        //Act
        var result = await commandHandler.Handle(command, new CancellationToken());

        //Assert
        result.Should().Be(command.Email.ToLower());
    }

    [Fact]
    public async Task Handle_RecordExists_ThrowsAlreadyExistException()
    {
        //Arrange
        var existingAccount = Fixture.Create<Account>();
        TestDbContext.Accounts.Add(existingAccount);
        await TestDbContext.SaveChangesAsync();

        var command = new AddAccountCommand
        {
            Name = existingAccount.Name,
            Email = existingAccount.Email,
            PhoneNumber = existingAccount.PhoneNumber,
            Claims = existingAccount.Claims
        };

        var commandHandler = new AddAccountCommandHandler(TestDbContext, MockLogger.Object);

        //Act and Assert
        await Assert.ThrowsAsync<AlreadyExistsException>(() => commandHandler.Handle(command, new CancellationToken()));
    }
}