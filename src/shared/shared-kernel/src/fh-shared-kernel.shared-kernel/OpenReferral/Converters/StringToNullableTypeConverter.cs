using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Converters;

/// <summary>
/// Converts a JSON string value into a nullable type.
/// </summary>
public class StringToNullableTypeConverter : JsonConverter<object?>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return true;
    }

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrEmpty(stringValue))
            {
                return null;
            }

            var targetType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;
            if (targetType == typeof(Guid))
            {
                return Convert.ChangeType(Guid.Parse(stringValue), targetType);
            }
            if(targetType == typeof(DateTime))
            {
                return Convert.ChangeType(DateTime.Parse(stringValue, CultureInfo.CurrentCulture), targetType);
            }

            if (targetType == typeof(TimeSpan))
            {
                return Convert.ChangeType(TimeSpan.Parse(stringValue, CultureInfo.CurrentCulture), targetType);
            }
            return Convert.ChangeType(stringValue, targetType);
        }

        return JsonSerializer.Deserialize(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteStringValue(value.ToString());
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}