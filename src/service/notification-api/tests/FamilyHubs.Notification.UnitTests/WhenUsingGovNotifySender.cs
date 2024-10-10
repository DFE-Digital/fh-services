using FamilyHubs.Notification.Data.NotificationServices;
using FamilyHubs.Notification.Api.Contracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.Notification.UnitTests;

public class WhenUsingGovNotifySender
{
    private readonly IServiceNotificationClient _serviceNotificationClient;
    private readonly GovNotifySender _govNotifySender;

    public WhenUsingGovNotifySender()
    {
        _serviceNotificationClient = Substitute.For<IServiceNotificationClient>();
        _govNotifySender = new GovNotifySender([_serviceNotificationClient],
            Substitute.For<ILogger<GovNotifySender>>());
    }

    [Theory]
    [InlineData(ApiKeyType.ConnectKey)]
    [InlineData(ApiKeyType.ManageKey)]
    public async Task ThenSendNotification(ApiKeyType apiKeyType)
    {
        //Arrange
        int sendEmailCallback = 0;
        _serviceNotificationClient.ApiKeyType.Returns(apiKeyType);
        _serviceNotificationClient.WhenForAnyArgs(x => x.SendEmailAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<Dictionary<string, object>>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>()))
            .Do(_ => sendEmailCallback++);

        MessageDto messageDto = new MessageDto
        {
            ApiKeyType = apiKeyType,
            NotificationEmails = ["someone@email.com"],
            TemplateId = Guid.NewGuid().ToString(),
            TemplateTokens = new Dictionary<string, string>
            {
                {"Key1", "Value1"},
                {"Key2", "Value2"}
            }
        };

        //Act
        await _govNotifySender.SendEmailAsync(messageDto);

        //Assert
        sendEmailCallback.Should().Be(1);
    }
}