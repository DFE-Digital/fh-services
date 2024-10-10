using FamilyHubs.Notification.Core.Commands.CreateNotification;
using FamilyHubs.Notification.Data.NotificationServices;
using FamilyHubs.Notification.Api.Contracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.Notification.UnitTests;

public class WhenUsingGovNotifyCommands : BaseCreateDbUnitTest
{
    private readonly ILogger<CreateNotificationCommandHandler> _logger = Substitute.For<ILogger<CreateNotificationCommandHandler>>();
    private readonly IGovNotifySender _govNotifySender = Substitute.For<IGovNotifySender>();

    private static readonly MessageDto MessageDto = new()
    {
        ApiKeyType = ApiKeyType.ConnectKey,
        NotificationEmails = ["someone@email.com"],
        TemplateId = Guid.NewGuid().ToString(),
        TemplateTokens = new Dictionary<string, string>
        {
            {"Key1", "Value1"},
            {"Key2", "Value2"}
        }
    };

    [Fact]
    public async Task ThenSendNotificationCommand()
    {
        //Arrange
        CreateNotificationCommand command = new CreateNotificationCommand(MessageDto);

        int sendEmailCallback = 0;
        _govNotifySender.WhenForAnyArgs(x => x.SendEmailAsync(Arg.Any<MessageDto>()))
            .Do(_ => sendEmailCallback++);

        var handler = new CreateNotificationCommandHandler(GetApplicationDbContext(), _govNotifySender, GetMapper(), _logger);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeTrue();
        sendEmailCallback.Should().Be(1);
    }

    [Fact]
    public async Task ThenSendNotificationCommandThrowsException()
    {
        //Arrange
        CreateNotificationCommand command = new CreateNotificationCommand(MessageDto);

        int sendEmailCallback = 0;
        _govNotifySender.WhenForAnyArgs(x => x.SendEmailAsync(Arg.Any<MessageDto>()))
            .Do(_ =>
            {
                sendEmailCallback++;
                throw new Exception();
            });

        var handler = new CreateNotificationCommandHandler(GetApplicationDbContext(), _govNotifySender, GetMapper(), _logger);

        //Act
        Func<Task> sutMethod = async () => { await handler.Handle(command, new CancellationToken()); };

        //Assert
        await sutMethod.Should().ThrowAsync<Exception>();
        sendEmailCallback.Should().Be(1);
    }
}
