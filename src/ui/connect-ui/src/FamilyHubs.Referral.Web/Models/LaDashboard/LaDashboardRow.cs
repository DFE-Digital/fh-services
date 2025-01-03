using System.Web;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.SharedKernel.Razor.Dashboard;

namespace FamilyHubs.Referral.Web.Models.LaDashboard;

public class LaDashboardRow : IRow<ReferralDto>
{
    private readonly string? _requestDetailsUrl;
    public ReferralDto Item { get; }

    public LaDashboardRow(ReferralDto referral, string? requestDetailsUrl)
    {
        _requestDetailsUrl = requestDetailsUrl;
        Item = referral;
    }

    public IEnumerable<ICell> Cells
    {
        get
        {
            yield return new Cell($"<a href=\"{_requestDetailsUrl}\">{HttpUtility.HtmlEncode(Item.RecipientDto.Name)}</a>");
            yield return new Cell(Item.ReferralServiceDto.Name);
            yield return new Cell(Item.LastModified?.ToString("dd MMM yyyy") ?? "");
            yield return new Cell(Item.Created?.ToString("dd MMM yyyy") ?? "");
            yield return new Cell(Item.Id.ToString("X6"));
            yield return new Cell(null, "Partials/_LaConnectionStatus");
        }
    }
}