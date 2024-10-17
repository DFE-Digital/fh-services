using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace FamilyHubs.SharedKernel.Razor.Summary;

public class SummaryCardTagHelper : TagHelper
{
    public string? Title { get; set; }
    public string? Action1 { get; set; }
    public string? Action1VisuallyHidden { get; set; }
    public string? Action1Href { get; set; }
    public string? Action2 { get; set; }
    public string? Action2VisuallyHidden { get; set; }
    public string? Action2Href { get; set; }
    public string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(Title);

        output.TagName = "div";
        output.Attributes.SetAttribute("class", $"govuk-summary-card {Class}");

        var childContent = (await output.GetChildContentAsync()).GetContent();

        StringBuilder actions = new();
        if (Action1 != null || Action2 != null)
        {
            actions.Append("<ul class=\"govuk-summary-card__actions\"");
            AddAction(actions, Action1, Action1VisuallyHidden, Action1Href);
            AddAction(actions, Action2, Action2VisuallyHidden, Action2Href);
            actions.Append("</ul>");
        }

        output.Content.SetHtmlContent($@"
            <div class=""govuk-summary-card__title-wrapper"">
                <h2 class=""govuk-summary-card__title"">{Title}</h2>
                {actions}
            </div>
            <div class=""govuk-summary-card__content"">
                <dl class=""govuk-summary-list"">
                    {childContent}
                </dl>
            </div>");
    }

    private static void AddAction(StringBuilder actions, string? action, string? visuallyHidden, string? href)
    {
        if (action == null)
            return;

        actions.Append("<li class=\"govuk-summary-card__action\">");
        actions.Append($"<a href=\"{href}\">{action}");
        if (visuallyHidden != null)
        {
            actions.Append($"<span class=\"govuk-visually-hidden\"> {visuallyHidden}</span>");
        }
        actions.Append("</a></li>");
    }
}