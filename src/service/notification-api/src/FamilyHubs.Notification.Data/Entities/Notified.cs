// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
namespace FamilyHubs.Notification.Data.Entities;

public class Notified : EntityBase<long>
{
    public required long NotificationId { get; set; }
    public required string Value { get; set; }
}
