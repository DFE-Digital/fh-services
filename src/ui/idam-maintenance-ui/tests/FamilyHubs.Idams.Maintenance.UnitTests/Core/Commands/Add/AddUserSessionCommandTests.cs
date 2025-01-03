using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FamilyHubs.Idams.Maintenance.Core.Exceptions;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Add;

public class AddUserSessionCommandTests
{
    [Fact]
    public async Task UserSessionExists_Handle_ThrowsAlreadyExistsException()
    {
        var userSessionList = TestUserSessions.GetListOfTestUserSessions();
        var repository = Substitute.For<IRepository>();
        repository.UserSessions.Returns(userSessionList.BuildMock());
        var logger = Substitute.For<ILogger<AddUserSessionCommandHandler>>();
        var handler = new AddUserSessionCommandHandler(repository, logger);
        var command = new AddUserSessionCommand { Sid = userSessionList[0].Sid, Email = userSessionList[0].Email };

        await Assert.ThrowsAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None)); 
    }
    
    [Fact]
    public async Task UserSessionDoesNotExist_Handle_AddsAccountAndSaves()
    {
        var userSessionList = TestUserSessions.GetListOfTestUserSessions();
        var repository = Substitute.For<IRepository>();
        repository.UserSessions.Returns(userSessionList.BuildMock());
        var logger = Substitute.For<ILogger<AddUserSessionCommandHandler>>();
        var handler = new AddUserSessionCommandHandler(repository, logger);
        var command = new AddUserSessionCommand { Sid = "Sid2", Email = userSessionList[0].Email };
        
        await handler.Handle(command, CancellationToken.None);
        
        await repository.Received().AddAsync(Arg.Is<UserSession>(us => us.Sid == command.Sid && us.Email == command.Email));
        await repository.Received().SaveChangesAsync(CancellationToken.None);
    }
}