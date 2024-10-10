using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Core.Commands.CreateNotification;
using FluentAssertions;

namespace FamilyHubs.Notification.UnitTests;

public class WhenValidatingNotificationCommands
{
    [Fact]
    public void ThenShouldNotErrorWhenCreateCommandCommandModelIsValid()
    {
        //Arrange
        var validator = new CreateNotificationCommandValidator();
        var testModel = new CreateNotificationCommand(new MessageDto
        {
            ApiKeyType = ApiKeyType.ConnectKey,
            NotificationEmails = ["someone@email.com"],
            TemplateId = "12e7463d-dbb5-4beb-a321-3ea5b55bb642",
            TemplateTokens = new Dictionary<string, string>
            {
                {  "Key1", "Value1" }
            }
        });

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Any().Should().BeFalse();
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldErrorWhenCreateCommandCommandModelHasNoRecipientEmail()
    {
        //Arrange
        var validator = new CreateNotificationCommandValidator();
        var testModel = new CreateNotificationCommand(new MessageDto
        {
            ApiKeyType = ApiKeyType.ConnectKey,
            NotificationEmails = [],
            TemplateId = "12e7463d-dbb5-4beb-a321-3ea5b55bb642",
            TemplateTokens = new Dictionary<string, string>
            {
                {  "Key1", "Value1" }
            }
        });

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ThenShouldErrorWhenCreateCommandCommandModelHasNoTemplateId()
    {
        //Arrange
        var validator = new CreateNotificationCommandValidator();
        var testModel = new CreateNotificationCommand(new MessageDto
        {
            ApiKeyType = ApiKeyType.ConnectKey,
            NotificationEmails = ["someone@email.com"],
            TemplateId = string.Empty,
            TemplateTokens = new Dictionary<string, string>
            {
                {  "Key1", "Value1" }
            }
        });

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
