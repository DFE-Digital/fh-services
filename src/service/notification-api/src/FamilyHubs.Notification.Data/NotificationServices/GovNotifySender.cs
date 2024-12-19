using FamilyHubs.Notification.Api.Contracts;
using Microsoft.Extensions.Logging;
using Notify.Exceptions;

namespace FamilyHubs.Notification.Data.NotificationServices;

public class GovNotifySender : IGovNotifySender
{
    private readonly IEnumerable<IServiceNotificationClient> _notificationClients;
    private readonly ILogger _logger;

    public GovNotifySender(
        IEnumerable<IServiceNotificationClient> notificationClients,
        ILogger<GovNotifySender> logger)
    {
        _notificationClients = notificationClients;
        _logger = logger;
    }

    public async Task SendEmailAsync(MessageDto messageDto)
    {
        var client = _notificationClients.FirstOrDefault(x => x.ApiKeyType == messageDto.ApiKeyType)
            ?? throw new InvalidOperationException($"Client for ApiKeyType {messageDto.ApiKeyType} not found");

        var personalisation = messageDto.TemplateTokens
            .ToDictionary(pair => pair.Key, dynamic? (pair) => pair.Value);

        foreach(var emailAddress in messageDto.NotificationEmails) 
        {
            _logger.LogInformation("Sending email");

            // make best effort to send notification to all recipients
            try
            {
                await client.SendEmailAsync(emailAddress, messageDto.TemplateId, personalisation);
            }
            catch (NotifyClientException e)
            {
                _logger.LogError(e, "An error occurred sending notification. {ExceptionMessage}", e.Message);
            }
        }
    }
}
