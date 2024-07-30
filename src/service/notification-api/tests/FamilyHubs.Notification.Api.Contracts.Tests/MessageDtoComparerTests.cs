namespace FamilyHubs.Notification.Api.Contracts.Tests;

public class MessageDtoComparerTests : PlainComparerTestBase<MessageDto, string>
{
    public MessageDtoComparerTests() : base(new MessageDto
    {
        ApiKeyType = ApiKeyType.ConnectKey,
        NotificationEmails = new List<string> { "someone@email.com", "someoneelse@email.com" },
        TemplateId = "3f23e8ee-7692-4716-aaab-770b69965977",
        TemplateTokens = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        }

    }, new MessageDto
    {
        ApiKeyType = ApiKeyType.ConnectKey,
        NotificationEmails = new List<string> { "someone@email.com", "someoneelse@email.com" },
        TemplateId = "3f23e8ee-7692-4716-aaab-770b69965977",
        TemplateTokens = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        }


    }, dto => dto.TemplateId)
    {

    }
}
