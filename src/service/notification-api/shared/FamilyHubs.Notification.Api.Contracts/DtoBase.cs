// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace FamilyHubs.Notification.Api.Contracts;

public record DtoBase<TId>
{
#pragma warning disable CS8618
    public TId Id { get; set; }
#pragma warning restore CS8618
}
