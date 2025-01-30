using System.Net;
using System.Text.RegularExpressions;
using FamilyHubs.SharedKernel.Utilities;

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

    public T Sanitize<T>(T input)
    {
        PropertyInspector.InspectStringProperties(input, (_, parent, property) =>
        {
            var value = (string?)property.GetValue(parent);
            if (value != null && property.CanWrite)
            {
                var sanitizedValue = Sanitize(value);
                property.SetValue(parent, sanitizedValue);
            }
        });
        return input;
    }

    public string Sanitize(string input)
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

    [GeneratedRegex("<script.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    private static partial Regex RegexJsScript();

    [GeneratedRegex(@"\s*on\w+\s*=\s*(['""]).*?\1", RegexOptions.IgnoreCase)]
    private static partial Regex RegexJsEvents();

    [GeneratedRegex(@"javascript\s*:\s*[^""]+", RegexOptions.IgnoreCase)]
    private static partial Regex RegexJavascriptUri();
    [GeneratedRegex("<.*?>")]
    private static partial Regex RegexHtmlTags();
}
