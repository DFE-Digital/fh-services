using FamilyHubs.Notification.Data.NotificationServices;
using FamilyHubs.Notification.Api.Contracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.Notification.UnitTests;

public class WhenUsingGovNotifySender
{
    [Fact]
    public async Task ThenSendConnectNotification()
    {
        //Arrange
        var mockAsyncNotificationClient = Substitute.For<IServiceNotificationClient>();
        var mockLogger = Substitute.For<ILogger<GovNotifySender>>();
        int sendEmailCallback = 0;
        mockAsyncNotificationClient.ApiKeyType.Returns(ApiKeyType.ConnectKey);
        mockAsyncNotificationClient.WhenForAnyArgs(x => x.SendEmailAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<Dictionary<string, dynamic>>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>()))
            .Do(_ => sendEmailCallback++);

        IEnumerable<IServiceNotificationClient> notificationClients = new List<IServiceNotificationClient>
            { mockAsyncNotificationClient };

        var govNotifySender = new GovNotifySender(notificationClients, mockLogger);
        var dict = new Dictionary<string, string>
        {
            {"Key1", "Value1"},
            {"Key2", "Value2"}
        };

        MessageDto messageDto = new MessageDto
        {
            ApiKeyType = ApiKeyType.ConnectKey,
            NotificationEmails = new List<string> { "someone@email.com" },
            TemplateId = Guid.NewGuid().ToString(),
            TemplateTokens = dict
        };

        //Act
        await govNotifySender.SendEmailAsync(messageDto);

        //Assert
        sendEmailCallback.Should().Be(1);
    }

    [Fact]
    public async Task ThenSendManageNotification()
    {
        //Arrange
        var mockAsyncNotificationClient = Substitute.For<IServiceNotificationClient>();
        var mockLogger = Substitute.For<ILogger<GovNotifySender>>();
        int sendEmailCallback = 0;
        mockAsyncNotificationClient.ApiKeyType.Returns(ApiKeyType.ManageKey);
        mockAsyncNotificationClient.WhenForAnyArgs(x => x.SendEmailAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<Dictionary<string, dynamic>>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>()))
            .Do(_ => sendEmailCallback++);

        IEnumerable<IServiceNotificationClient> notificationClients = new List<IServiceNotificationClient>() { mockAsyncNotificationClient };

        var govNotifySender = new GovNotifySender(notificationClients, mockLogger);
        var dict = new Dictionary<string, string>
        {
            {"Key1", "Value1"},
            {"Key2", "Value2"}
        };

        MessageDto messageDto = new MessageDto
        {
            ApiKeyType = ApiKeyType.ManageKey,
            NotificationEmails = new List<string> { "someone@email.com" },
            TemplateId = Guid.NewGuid().ToString(),
            TemplateTokens = dict
        };

        //Act
        await govNotifySender.SendEmailAsync(messageDto);

        //Assert
        sendEmailCallback.Should().Be(1);
    }
}