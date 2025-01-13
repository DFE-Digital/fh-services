using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.SharedKernel.Razor.FamilyHubsUi;

public class FamilyHubsLayoutModel
{
    //todo: have IFamilyHubsUiOptions directly here?
    public IOptions<FamilyHubsUiOptions> FamilyHubsUiOptions { get; set; }

    public FamilyHubsLayoutModel(IOptions<FamilyHubsUiOptions> familyHubsUiOptions)
    {
        FamilyHubsUiOptions = familyHubsUiOptions;
    }

    public bool IsError { get; set; }

    public PageModel? PageModel { get; set; }
}