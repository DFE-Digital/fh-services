using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public abstract class BaseHsdsEntity
{
    public Guid Id { get; init; }
    public required Guid OrId { get; init; }
}