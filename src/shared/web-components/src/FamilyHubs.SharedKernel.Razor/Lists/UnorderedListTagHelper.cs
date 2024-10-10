using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace FamilyHubs.SharedKernel.Razor.Lists;

[HtmlTargetElement("govuk-ul")]
public class UnorderedListTagHelper : TagHelper
{
    /// <summary>
    /// An optional list of items to display in the list.
    /// </summary>
    public IEnumerable<string>? Items { get; set; }

    public string? Class { get; set; }

    public bool Bulleted { get; set; }

    /// <summary>
    /// Add extra spacing between list items.
    /// </summary>
    public bool Spaced { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";

        output.Attributes.SetAttribute("class", $"govuk-list {(Bulleted ? "govuk-list--bullet" : "")} {(Spaced ? "govuk-list--spaced" : "")} {Class}");

        string content;
        if (Items != null)
        {
            var sb = new StringBuilder();
            foreach (var item in Items)
            {
                sb.Append($"<li>{item}</li>");
            }

            content = sb.ToString();
        }
        else
        {
            content = (await output.GetChildContentAsync()).GetContent();
        }
        output.Content.SetHtmlContent(content);
    }
}