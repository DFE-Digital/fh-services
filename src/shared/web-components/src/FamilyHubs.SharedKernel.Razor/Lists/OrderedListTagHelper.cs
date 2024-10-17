using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace FamilyHubs.SharedKernel.Razor.Lists;

[HtmlTargetElement("govuk-ol")]
public class OrderedListTagHelper : TagHelper
{
    /// <summary>
    /// An optional list of items to display in the list.
    /// </summary>
    public IEnumerable<string>? Items { get; set; }

    public string? Class { get; set; }

    /// <summary>
    /// Add extra spacing between list items.
    /// </summary>
    public bool Spaced { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ol";

        output.Attributes.SetAttribute("class", $"govuk-list govuk-list--number {(Spaced ? "govuk-list--spaced" : "")} {Class}");

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