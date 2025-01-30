using System.Collections;
using System.Reflection;

namespace FamilyHubs.SharedKernel.Utilities;

public static class PropertyInspector
{
    public static void InspectStringProperties(object? obj, Action<string, object, PropertyInfo> action)
    {
        if (obj == null) return;
        var type = obj.GetType();

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var value = property.GetValue(obj)!;
            
            if (property.PropertyType == typeof(string))
            {
                action(property.Name, obj, property); // Perform action on string properties
            }
            else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && 
                     property.PropertyType != typeof(string)) // Handle collections
            {
                if (value is IEnumerable collection)
                {
                    foreach (var item in collection)
                    {
                        InspectStringProperties(item, action);
                    }
                }
            }
            else if (property.PropertyType.IsClass) // Handle nested objects
            {
                InspectStringProperties(value, action);
            }
        }
    }
}