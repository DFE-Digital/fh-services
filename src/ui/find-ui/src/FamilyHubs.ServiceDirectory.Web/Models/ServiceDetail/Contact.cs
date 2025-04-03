namespace FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail;

public class Contact
{
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? Website { get; init; }
    public string? TextMessage { get; init; }

    private bool Equals(Contact other)
    {
        return Email == other.Email &&
               Phone == other.Phone &&
               Website == other.Website &&
               TextMessage == other.TextMessage;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Contact)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Email, Phone, Website, TextMessage);
    }
}