using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Core.Commands.CreateNotification;
using FamilyHubs.Notification.Data.NotificationServices;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace FamilyHubs.Notification.IntegrationTests;

public class WhenCreatingNotifications : DataIntegrationTestBase
{
    [Theory]
    [InlineData(ApiKeyType.ManageKey)]
    [InlineData(ApiKeyType.ConnectKey)]
    public async Task ThenCreateNotification(ApiKeyType apiKeyType)
    {
        var createNotificationCommand = new CreateNotificationCommand(new MessageDto
        {
            Id = 1,
            ApiKeyType = apiKeyType,
            NotificationEmails = new List<string> { "firstperson@aol.com", "secondperson@aol.com" },
            TemplateId = "05d38535-a5c3-443e-bfde-54f2abdd5c78",
            TemplateTokens = new Dictionary<string, string>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" },
            }

        });

        var logger = new Mock<ILogger<CreateNotificationCommandHandler>>();
        Mock<IGovNotifySender> govNotifySender = new Mock<IGovNotifySender>();
        int sendEmailCallback = 0;
        govNotifySender.Setup(x => x.SendEmailAsync(It.IsAny<MessageDto>()))
            .Callback(() => sendEmailCallback++);

        var handler = new CreateNotificationCommandHandler(TestDbContext, govNotifySender.Object, Mapper, logger.Object);

        //Act
        var result = await handler.Handle(createNotificationCommand, new CancellationToken());
        result.Should().BeTrue();
        var actualNotification = TestDbContext.SentNotifications
            .Include(x => x.Notified)
            .Include(x => x.TokenValues)
            .SingleOrDefault(x => x.Id == createNotificationCommand.MessageDto.Id);
        ArgumentNullException.ThrowIfNull(actualNotification);
        actualNotification.ApiKeyType.Should().Be(apiKeyType);
        actualNotification.TemplateId.Should().Be(createNotificationCommand.MessageDto.TemplateId);
        foreach (var token in createNotificationCommand.MessageDto.TemplateTokens)
        {
            var tokenValue = actualNotification.TokenValues.FirstOrDefault(x => x.Key == token.Key);
            ArgumentNullException.ThrowIfNull(tokenValue);
            tokenValue.Value.Should().Be(token.Value);
        }
        actualNotification.Notified.Should().Contain(x => x.Value == createNotificationCommand.MessageDto.NotificationEmails[0]);
        actualNotification.Notified.Should().Contain(x => x.Value == createNotificationCommand.MessageDto.NotificationEmails[1]);
        sendEmailCallback.Should().Be(1);
    }
}