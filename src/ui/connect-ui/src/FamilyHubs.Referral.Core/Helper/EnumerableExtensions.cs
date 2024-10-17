using FamilyHubs.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHubs.Referral.Core.Helper;

public static class EnumerableExtensions
{
    public static string ToDisplay<TEnum>(this IEnumerable<TEnum> enumValues)
        where TEnum : Enum
    {
        var orderedDescriptions = enumValues
            .Select(v => v.ToDescription())
            .OrderBy(d => d)
            .ToArray();

        string display = orderedDescriptions.Length switch
        {
            0 => string.Empty,
            1 => orderedDescriptions[0],
            2 => string.Join(" and ", orderedDescriptions),
            _ => string.Join(", ", orderedDescriptions.Take(orderedDescriptions.Length - 2))
                 + $", {orderedDescriptions[^2]} and {orderedDescriptions[^1]}"
        };

        return display.Length > 0 ? char.ToUpper(display[0]) + display[1..].ToLower() : display;
    }
}