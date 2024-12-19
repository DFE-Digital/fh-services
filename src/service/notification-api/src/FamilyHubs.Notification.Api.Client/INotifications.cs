using FamilyHubs.Notification.Api.Contracts;

namespace FamilyHubs.Notification.Api.Client;

public interface INotifications
{
    Task SendEmailsAsync(
        IEnumerable<string> emailAddresses,
        string templateId,
        IDictionary<string, string?> tokens,
        ApiKeyType apiKeyType = ApiKeyType.ConnectKey,
        CancellationToken cancellationToken = default);
}