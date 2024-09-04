using Ardalis.GuardClauses;
using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Core.Queries.GetSentNotifications;
using FamilyHubs.Notification.Data.Entities;
using FluentAssertions;

namespace FamilyHubs.Notification.UnitTests;

public class WhenGettingSentNotifications : BaseCreateDbUnitTest
{

    [Theory]
    [InlineData(NotificationOrderBy.ApiKeyType, true, 1)]
    [InlineData(NotificationOrderBy.ApiKeyType, false, 2)]
    [InlineData(NotificationOrderBy.RecipientEmail, true, 1)]
    [InlineData(NotificationOrderBy.RecipientEmail, false, 2)]
    [InlineData(NotificationOrderBy.Created, true, 1)]
    [InlineData(NotificationOrderBy.Created, false, 2)]
    [InlineData(NotificationOrderBy.TemplateId, true, 1)]
    [InlineData(NotificationOrderBy.TemplateId, false, 2)]
    public async Task ThenGetSentNotifications(NotificationOrderBy orderBy, bool isAscending, int firstId)
    {
        //Arrange
        GetNotificationsCommand command = new GetNotificationsCommand(null, orderBy, isAscending, 1, 10);
        using var context = GetApplicationDbContext();
        context.AddRange(GetNotificationList());
        context.SaveChanges();


        GetNotificationsCommandHandler handler = new GetNotificationsCommandHandler(context, GetMapper());

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(2);
        result.Items[0].Created.Should().NotBeNull();
        result.Items[0].Id.Should().Be(firstId);
    }

    
    [Theory]
    [InlineData(null)]
    [InlineData(ApiKeyType.ConnectKey)]
    [InlineData(ApiKeyType.ManageKey)]
    public async Task ThenGetSentNotificationsByApiKeyType(ApiKeyType? apiKeyType)
    {
        GetNotificationsCommand command = new GetNotificationsCommand(apiKeyType, null, false, 1, 10);
        using var context = GetApplicationDbContext();
        context.AddRange(GetNotificationList());
        context.SaveChanges();


        GetNotificationsCommandHandler handler = new GetNotificationsCommandHandler(context, GetMapper());

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        if (apiKeyType != null)
            result.Items.Count.Should().Be(1);
        else
            result.Items.Count.Should().Be(2);
        result.Items[0].Created.Should().NotBeNull();
    }

    [Fact]
    public async Task ThenGetSentNotificationById()
    {
        //Arrange
        var expected = GetMapper().Map<MessageDto>(GetNotificationList().First(x => x.Id == 1));
        using var context = GetApplicationDbContext();
        context.AddRange(GetNotificationList());
        context.SaveChanges();

        GetNotificationByIdCommand command = new(1);
        GetNotificationByIdCommandHandler handler = new(context, GetMapper());

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.Created));
        
    }

    [Fact]
    public async Task ThenGetSentNotificationById_ReturnsNotFoundException()
    {
        //Arrange
        using var context = GetApplicationDbContext();
        GetNotificationByIdCommand command = new(1);
        GetNotificationByIdCommandHandler handler = new(context, GetMapper());

        //Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    private List<SentNotification> GetNotificationList()
    {
        return new List<SentNotification>
        {
            new SentNotification
            {
                Id = 1,
                ApiKeyType = ApiKeyType.ManageKey,
                Notified = new List<Notified>
                {
                    new Notified
                    {
                        Id = 1,
                        NotificationId = 1,
                        Value = "Firstperson@email.com"
                    }
                },
                TemplateId = "11111",
                TokenValues = new List<TokenValue>
                {
                    new TokenValue
                    {
                        Id = 1,
                        NotificationId = 1,
                        Key = "Key1",
                        Value = "Value1"
                    }
                }
               
            },

            new SentNotification
            {
                Id = 2,
                ApiKeyType = ApiKeyType.ConnectKey,
                Notified = new List<Notified>
                {
                    new Notified
                    {
                        Id = 2,
                        NotificationId = 2,
                        Value = "Secondperson@email.com"
                    }
                },
                TemplateId = "2222",
                TokenValues = new List<TokenValue>
                {
                    new TokenValue
                    {
                        Id = 2,
                        NotificationId = 2,
                        Key = "Key2",
                        Value = "Value2"
                    }
                }

            },
        };
    }
}
