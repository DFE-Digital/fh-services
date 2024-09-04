// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable UnusedMember.Global
// ReSharper disable UsageOfDefaultStructEquality
namespace FamilyHubs.Notification.Api.Contracts;

public record MessageDto : DtoBase<long>
{
    public required ApiKeyType ApiKeyType { get; set; }
    public required List<string> NotificationEmails { get; set; }
    public required string TemplateId { get; set; }
    public Dictionary<string, string> TemplateTokens { get; set; } = new();
    public DateTime? Created { get; set; }

    public override int GetHashCode()
    {
        int result = 0;
        foreach (var token in TemplateTokens)
        {
            result += EqualityComparer<KeyValuePair<string, string>>.Default.GetHashCode(token);
        }

        result +=
            EqualityComparer<string?>.Default.GetHashCode(ApiKeyType.ToString()) * -1521134295 +
            EqualityComparer<string?>.Default.GetHashCode(string.Join(',',NotificationEmails)) * -1521134295 +
            EqualityComparer<string?>.Default.GetHashCode(TemplateId) * -1521134295;

        foreach (var token in TemplateTokens)
        {
            result +=
            EqualityComparer<string?>.Default.GetHashCode(token.Key) * -1521134295 +
            EqualityComparer<string?>.Default.GetHashCode(token.Value) * -1521134295;
        }

        return result;
    }

    public virtual bool Equals(MessageDto? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other))
            return true;

        var keys = TemplateTokens.Select(x => x.Key);

        foreach (var key in keys)
        {
            if (!other.TemplateTokens.ContainsKey(key))
                return false;


            if (!EqualityComparer<string>.Default.Equals(TemplateTokens[key], other.TemplateTokens[key]))
                return false;

        }

        return
            EqualityComparer<ApiKeyType>.Default.Equals(ApiKeyType, other.ApiKeyType) &&
            NotificationEmails.SequenceEqual(other.NotificationEmails) &&
            EqualityComparer<string>.Default.Equals(TemplateId, other.TemplateId);
    }
}

