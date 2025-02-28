using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models.ServiceJourney;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

public class Locations_For_ServiceModel : ServicePageModel
{
    // don't change the const "sub", otherwise it might trigger a GA bug
    // that incorrectly rewrites the URL on post back, breaking the post back

    // \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    public const string SubmitAction = "sub"; // << DON'T CHANGE THIS!
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

    public const string SubmitAction_Continue = "continue";
    public const string SubmitAction_AddAnotherLocation = "add";

    public IEnumerable<ServiceLocationModel> Locations { get; private set; } = Enumerable.Empty<ServiceLocationModel>();

    public Locations_For_ServiceModel(
        IRequestDistributedCache connectionRequestCache)
        : base(ServiceJourneyPage.Locations_For_Service, connectionRequestCache)
    {
    }

    protected override async Task OnGetWithModelAsync(CancellationToken cancellationToken)
    {
        Locations = ServiceModel!.AllLocations;

        await ResurrectCurrentLocation();
    }

    /// <summary>
    /// This is to handle the scenario where the user has clicked add location from this page,
    /// then clicked back to this page and then will click back again to the location extra details page.
    /// Without resetting the current location, the location extra details page won't have
    /// a current location to work with and will explode.
    /// </summary>
    /// <returns></returns>
    private async Task ResurrectCurrentLocation()
    {
        if (ServiceModel!.CurrentLocation == null
            && ServiceModel.Locations.Any())
        {
            //todo: this belongs as a method on ServiceModel
            ServiceModel.CurrentLocation = ServiceModel.Locations[^1];
            ServiceModel.Locations.Remove(ServiceModel.CurrentLocation);
            await Cache.SetAsync(FamilyHubsUser.Email, ServiceModel);
        }
    }

    protected override IActionResult OnPostWithModel()
    {
        string action = Request.Form[SubmitAction].ToString();
        if (action == SubmitAction_AddAnotherLocation)
        {
            ServiceModel!.MoveCurrentLocationToLocations();

            return Redirect(GetServicePageUrl(ServiceJourneyPage.Select_Location, ChangeFlow, CurrentPage));
        }
        return NextPage();
    }
}
