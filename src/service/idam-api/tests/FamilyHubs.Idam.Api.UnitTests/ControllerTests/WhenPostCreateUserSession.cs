using FamilyHubs.Idam.Api.Controllers;
using FamilyHubs.Idam.Core.Commands.Add;
using FamilyHubs.Idam.Core.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace FamilyHubs.Idam.Api.UnitTests.ControllerTests;

public class WhenPostCreateUserSession
{
    private readonly UserSessionController _sut;
    private readonly IMediator _mediator;

    public WhenPostCreateUserSession()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new UserSessionController(_mediator)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Fact]
    public async Task ShouldCallDeleteExpiredFirstBeforeTryingToAddSession()
    {
        // Arrange
        var addUserSessionCommand = new AddUserSessionCommand
        {
            Sid = "aSid",
            Email = "ASidEmail@sid.com"
        };
        _mediator.Send(addUserSessionCommand, CancellationToken.None)
            .Returns("sid");

        // Act
        await _sut.Create(addUserSessionCommand, CancellationToken.None);

        // Assert
        Received.InOrder(() =>
        {
            _mediator.Send(Arg.Any<DeleteExpiredUserSessionsCommand>(), CancellationToken.None);
            _mediator.Send(addUserSessionCommand, CancellationToken.None);
        });
    }
    
    [Fact]
    public async Task ShouldReturnSidWhenSessionIsCreated()
    {
        // Arrange
        var addUserSessionCommand = new AddUserSessionCommand
        {
            Sid = "aSid",
            Email = "ASidEmail@sid.com"
        };
        _mediator.Send(addUserSessionCommand, CancellationToken.None)
            .Returns("sid");

        // Act
        var result = await _sut.Create(addUserSessionCommand, CancellationToken.None);

        // Assert
        Assert.Equal("sid", result);
    }
}