using Ardalis.GuardClauses;
using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Core.Queries.GetSentNotifications;
using FamilyHubs.Notification.Data.Repository;
using FluentAssertions;

namespace FamilyHubs.Notification.UnitTests;

public class WhenGettingSentNotifications : BaseCreateDbUnitTest
{
    private ApplicationDbContext ApplicationDbContext { get; set; } = null!;

    private void Setup(bool withNoData)
    {
        ApplicationDbContext = GetApplicationDbContext();

        if (withNoData) return;

        ApplicationDbContext.AddRange(NotificationList);
        ApplicationDbContext.SaveChanges();
    }

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
        Setup(false);
        GetNotificationsCommand command = new GetNotificationsCommand(null, orderBy, isAscending, 1, 10);
        GetNotificationsCommandHandler handler = new GetNotificationsCommandHandler(ApplicationDbContext, GetMapper());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

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
        //Arrange
        Setup(false);
        GetNotificationsCommand command = new GetNotificationsCommand(apiKeyType, null, false, 1, 10);
        GetNotificationsCommandHandler handler = new GetNotificationsCommandHandler(ApplicationDbContext, GetMapper());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(apiKeyType is not null ? 1 : 2);
        result.Items[0].Created.Should().NotBeNull();
    }

    [Fact]
    public async Task ThenGetSentNotificationById()
    {
        //Arrange
        Setup(false);
        var expected = GetMapper().Map<MessageDto>(NotificationList.First(x => x.Id == 1));

        GetNotificationByIdCommand command = new(1);
        GetNotificationByIdCommandHandler handler = new(ApplicationDbContext, GetMapper());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.Created));
        
    }

    [Fact]
    public async Task ThenGetSentNotificationById_ReturnsNotFoundException()
    {
        //Arrange
        Setup(true);
        GetNotificationByIdCommand command = new(1);
        GetNotificationByIdCommandHandler handler = new(ApplicationDbContext, GetMapper());

        //Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
