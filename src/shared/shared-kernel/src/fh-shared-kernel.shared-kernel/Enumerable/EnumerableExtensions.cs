using FamilyHubs.SharedKernel.Enums;

namespace FamilyHubs.SharedKernel.Enumerable;

public static class EnumerableExtensions
{
    public static string ToDisplay<TEnum>(this IEnumerable<TEnum> enumValues)
        where TEnum : Enum
    {
        if (!enumValues.Any())
        {
            return "";
        }

        var orderedDescriptions = enumValues
            .Select(v => v.ToDescription())
            .OrderBy(d => d)
            .ToArray();

        string display = orderedDescriptions.Length switch
        {
            1 => orderedDescriptions[0],
            2 => string.Join(" and ", orderedDescriptions),
            _ => $"{string.Join(", ", orderedDescriptions.Take(orderedDescriptions.Length - 2))}, {orderedDescriptions[^2]} and {orderedDescriptions[^1]}"
        };

        return display.Length > 0 ? char.ToUpper(display[0]) + display[1..].ToLower() : display;
    }
}