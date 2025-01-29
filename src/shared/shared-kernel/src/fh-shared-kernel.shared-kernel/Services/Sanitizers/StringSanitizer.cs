using System.Net;
using System.Text.RegularExpressions;

namespace FamilyHubs.SharedKernel.Services.Sanitizers;



public interface IStringSanitizer
{
    T Sanitize<T>(T input);
    string Sanitize(string input);
}

/// <summary>
/// Builder class for sanitizing strings and all string properties of a class 
/// </summary>
public partial class StringSanitizer : IStringSanitizer
{
    private bool _removeHtml;
    private bool _removeJs;
    private bool _removeProfanity;

    internal StringSanitizer RemoveHtml()
    {
        _removeHtml = true;
        return this;
    }
    
    internal StringSanitizer RemoveJs()
    {
        _removeJs = true;
        return this;
    }
    
    internal StringSanitizer RemoveProfanity()
    {
        _removeProfanity = true;
        return this;
    }

    public T Sanitize<T>(T input)
    {
        return SanitizeStringGeneric(input);
    }

    public string Sanitize(string input)
    {
        return SanitizeString(input);
    }
    
    private string SanitizeString(string input)
    {
        var sanitizedHtml = input;
        
        if (_removeJs)
        {
            sanitizedHtml = RemoveJavascript(sanitizedHtml);
        }
        
        if (_removeHtml)
        { 
            sanitizedHtml = RemoveHtml(sanitizedHtml);
        }

        if (_removeProfanity)
        {
            sanitizedHtml = RemoveProfanity(sanitizedHtml);
        }

        return sanitizedHtml;
    }
    
    private static string RemoveProfanity(string input)
    {
        var sanitizedHtml = input;
        var profanityFilter = new ProfanityFilter.ProfanityFilter();
        var allProfanities = profanityFilter.DetectAllProfanities(input);
        
        if (allProfanities is not null && allProfanities.Count > 0)
        {
            sanitizedHtml = allProfanities.Aggregate(input, 
                (current, profanity) => current.Replace(profanity, ""));
        }

        return sanitizedHtml;
    }
    
    private static string RemoveJavascript(string input)
    {
        // Remove <script> tags and their content
        var stripTags = RegexJsScript().Replace(input, string.Empty);

        // Remove event handler attributes (e.g., onclick, onmouseover)
        stripTags = RegexJsEvents().Replace(stripTags, string.Empty);

        // Remove javascript: URIs
        stripTags = RegexJavascriptUri().Replace(stripTags, string.Empty);

        return stripTags;
    }
    
    private static string RemoveHtml(string input)
    {
        var stripTags = RegexHtmlTags().Replace(input, string.Empty);
        stripTags = WebUtility.HtmlDecode(stripTags);
        stripTags = stripTags.Replace("\u00A0", " "); // Replace non-breaking space with regular space
        return stripTags;
    }
    
    private T SanitizeStringGeneric<T>(T input)
    {
        if (input is null)
        {
            return input;
        }

        var properties = input.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Ensure property is writable and has a setter
            if (!property.CanWrite)
            {
                continue;
            }

            try
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string?)property.GetValue(input);
                    if (value != null)
                    {
                        var sanitizedValue = Sanitize(value);
                        property.SetValue(input, sanitizedValue);
                    }
                }
                else if (!property.PropertyType.IsValueType && property.PropertyType != typeof(string))
                {
                    // Handle nested objects
                    var nestedObject = property.GetValue(input);
                    if (nestedObject != null)
                    {
                        var sanitizedNestedObject = Sanitize(nestedObject);
                        
                        // This is reflection so it bypasses the init readonly allowing us to set the value
                        property.SetValue(input, sanitizedNestedObject);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle any exceptions gracefully
                Console.WriteLine($"Error sanitizing property {property.Name}: {ex.Message}");
            }
        }
        return input;
    }

    [GeneratedRegex("<script.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    private static partial Regex RegexJsScript();

    [GeneratedRegex(@"\s*on\w+\s*=\s*(['""]).*?\1", RegexOptions.IgnoreCase)]
    private static partial Regex RegexJsEvents();

    [GeneratedRegex(@"javascript\s*:\s*[^""]+", RegexOptions.IgnoreCase)]
    private static partial Regex RegexJavascriptUri();
    [GeneratedRegex("<.*?>")]
    private static partial Regex RegexHtmlTags();
}
