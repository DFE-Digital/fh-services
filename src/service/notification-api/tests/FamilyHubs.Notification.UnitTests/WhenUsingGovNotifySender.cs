using FamilyHubs.Notification.Data.NotificationServices;
using FamilyHubs.Notification.Api.Contracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace FamilyHubs.Notification.UnitTests;

public class WhenUsingGovNotifySender
{
    [Fact]
    public async Task ThenSendConnectNotification()
    {
        //Arrange
        var mockAsyncNotificationClient = new Mock<IServiceNotificationClient>();
        var mockLogger = new Mock<ILogger<GovNotifySender>>();
        int sendEmailCallback = 0;
        mockAsyncNotificationClient.Setup(c => c.ApiKeyType).Returns(ApiKeyType.ConnectKey);
        mockAsyncNotificationClient.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, dynamic>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Callback(() => sendEmailCallback++);

        IEnumerable<IServiceNotificationClient> notificationClients = new List<IServiceNotificationClient>
            { mockAsyncNotificationClient.Object };

        var govNotifySender = new GovNotifySender(notificationClients, mockLogger.Object);
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
        var mockAsyncNotificationClient = new Mock<IServiceNotificationClient>();
        var mockLogger = new Mock<ILogger<GovNotifySender>>();
        int sendEmailCallback = 0;
        mockAsyncNotificationClient.Setup(c => c.ApiKeyType).Returns(ApiKeyType.ManageKey);
        mockAsyncNotificationClient.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, dynamic>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Callback(() => sendEmailCallback++);

        IEnumerable<IServiceNotificationClient> notificationClients = new List<IServiceNotificationClient>() { mockAsyncNotificationClient.Object };

        var govNotifySender = new GovNotifySender(notificationClients, mockLogger.Object);
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