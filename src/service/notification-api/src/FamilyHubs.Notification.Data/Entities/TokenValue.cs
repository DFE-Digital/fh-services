// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
namespace FamilyHubs.Notification.Data.Entities
{
    public class TokenValue : EntityBase<long>
    {
        public required long NotificationId { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
