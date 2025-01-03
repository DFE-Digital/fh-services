using System.Web;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.SharedKernel.Razor.Dashboard;

namespace FamilyHubs.Referral.Web.Models.VcsDashboard;

public class VcsDashboardRow : IRow<ReferralDto>
{
    private readonly string? _requestDetailsUrl;
    public ReferralDto Item { get; }

    public VcsDashboardRow(ReferralDto referral, string? requestDetailsUrl)
    {
        _requestDetailsUrl = requestDetailsUrl;
        Item = referral;
    }

    public IEnumerable<ICell> Cells
    {
        get
        {
            yield return new Cell($"<a href=\"{_requestDetailsUrl}\">{HttpUtility.HtmlEncode(Item.RecipientDto.Name)}</a>");
            yield return new Cell(Item.Created?.ToString("dd MMM yyyy") ?? "");
            yield return new Cell(Item.Id.ToString("X6"));
            yield return new Cell(null, "Partials/_VcsConnectionStatus");
        }
    }
}