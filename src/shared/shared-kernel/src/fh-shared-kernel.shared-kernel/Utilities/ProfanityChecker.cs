using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace FamilyHubs.SharedKernel.Utilities;

public static class ProfanityChecker
{
    private static readonly ProfanityFilter.ProfanityFilter ProfanityFilter = GetProfanityFilter();

    private static ProfanityFilter.ProfanityFilter GetProfanityFilter()
    {
        var profanityFilter = new ProfanityFilter.ProfanityFilter();
        
        return profanityFilter;
    }

    public static bool HasProfanity<T>([DisallowNull]T obj) where T : class
    {
        var hasProfanity = false;
        
        if(typeof(T) == typeof(string))
        {
            if (obj is string value && ProfanityFilter.DetectAllProfanities(value).Count > 0)
            {
                hasProfanity = true;
            }
            return hasProfanity;
        }
        
        PropertyInspector.InspectStringProperties(obj, (propertyName, parent, property) =>
        {
            var value = (string?)property.GetValue(parent);
            
            // Using DetectAllProfanities method from ProfanityFilter library to check for profanity as it only picks on whole words
            // The ContainsProfanity method from the same library picks up profanity in the middle of words and is too aggressive
            if (value is  not null && ProfanityFilter.DetectAllProfanities(value).Count > 0)
            {
                hasProfanity = true;
            }
        });
        return hasProfanity;
    }
}

