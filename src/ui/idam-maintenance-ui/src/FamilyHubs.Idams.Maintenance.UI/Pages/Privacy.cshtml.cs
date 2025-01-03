using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

[ExcludeFromCodeCoverage]
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}