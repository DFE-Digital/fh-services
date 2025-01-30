using System.Diagnostics.CodeAnalysis;

namespace FamilyHubs.SharedKernel.Utilities;

public static class ProfanityChecker
{
    private static readonly ProfanityFilter.ProfanityFilter ProfanityFilter = new();

    public static bool HasProfanity<T>([DisallowNull]T obj) where T : class
    {
        var hasProfanity = false;
        PropertyInspector.InspectStringProperties(obj, (propertyName, parent, property) =>
        {
            var value = (string?)property.GetValue(parent);
            if (value != null && ProfanityFilter.ContainsProfanity(value))
            {
                hasProfanity = true;
            }
        });
        return hasProfanity;
    }
}

