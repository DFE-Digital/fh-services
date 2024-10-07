using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Core.Commands.CreateNotification;
using FamilyHubs.Notification.Data.NotificationServices;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.Notification.IntegrationTests;

public class WhenCreatingNotifications : DataIntegrationTestBase
{
    [Theory]
    [InlineData(ApiKeyType.ManageKey)]
    [InlineData(ApiKeyType.ConnectKey)]
    public async Task ThenCreateNotification(ApiKeyType apiKeyType)
    {
        //Arrange
        var createNotificationCommand = new CreateNotificationCommand(new MessageDto
        {
            Id = 1,
            ApiKeyType = apiKeyType,
            NotificationEmails = ["firstperson@aol.com", "secondperson@aol.com"],
            TemplateId = "05d38535-a5c3-443e-bfde-54f2abdd5c78",
            TemplateTokens = new Dictionary<string, string>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            }
        });

        var handler = new CreateNotificationCommandHandler(
            TestDbContext,
            Substitute.For<IGovNotifySender>(),
            Mapper,
            Substitute.For<ILogger<CreateNotificationCommandHandler>>());

        //Act
        var result = await handler.Handle(createNotificationCommand, CancellationToken.None);

        result.Should().BeTrue();

        var query = TestDbContext.SentNotifications
            .Include(sentNotification => sentNotification.TokenValues)
            .Include(sentNotification => sentNotification.Notified);

        var actualNotification = await query.SingleOrDefaultAsync();

        actualNotification.Should().NotBeNull();
        actualNotification!.ApiKeyType.Should().Be(apiKeyType);
        actualNotification.TemplateId.Should().Be(createNotificationCommand.MessageDto.TemplateId);

        foreach (var token in createNotificationCommand.MessageDto.TemplateTokens)
        {
            var tokenValue = actualNotification.TokenValues.FirstOrDefault(x => x.Key == token.Key);
            tokenValue.Should().NotBeNull();
            tokenValue!.Value.Should().Be(token.Value);
        }

        actualNotification.Notified.Should()
            .Contain(x => x.Value == createNotificationCommand.MessageDto.NotificationEmails[0]);
        actualNotification.Notified.Should()
            .Contain(x => x.Value == createNotificationCommand.MessageDto.NotificationEmails[1]);
    }
}