using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Web;
using FamilyHubs.Referral.Core.DistributedCache;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.Referral.Core.ValidationAttributes;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FamilyHubs.Referral.Web.Pages.ProfessionalReferral;

public class LetterModel : ProfessionalReferralCacheModel
{
    //todo: consistency with nullable
    [BindProperty]
    [Required(ErrorMessage = "Enter the first line of the address")]
    public string? AddressLine1 { get; set; } = "";

    [BindProperty]
    public string? AddressLine2 { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "Enter a town or city")]
    public string? TownOrCity { get; set; } = "";

    [BindProperty]
    public string? County { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "Enter a postcode")]
    [UkGdsPostcode]
    public string? Postcode { get; set; } = "";

    public string HeadingText { get; set; } = "";

    public LetterModel(IConnectionRequestDistributedCache connectionRequestCache)
        : base(ConnectJourneyPage.Letter, connectionRequestCache)
    {
    }

    protected override void OnGetWithModel(ConnectionRequestModel model)
    {
        AddressLine1 = model.AddressLine1;
        AddressLine2 = model.AddressLine2;
        TownOrCity = model.TownOrCity;
        County = model.County;
        Postcode = model.Postcode;

        SetPageProperties(model);
    }

    private readonly ImmutableDictionary<string, ErrorId> _propertyToErrorId =
        ImmutableDictionary.Create<string, ErrorId>()
            .Add(nameof(AddressLine1), ErrorId.Letter_AddressLine1)
            .Add(nameof(TownOrCity), ErrorId.Letter_TownOrCity)
            .Add(nameof(Postcode), ErrorId.Letter_Postcode);

    private IErrorState GetErrors(params string[] propertyNames)
    {
        var invalidProperties = propertyNames.Select(p => (propertyName: p, entry: ModelState[p]))
            .Where(t => t.entry!.ValidationState == ModelValidationState.Invalid)
            .ToList();

        var errors = invalidProperties
            .ToImmutableDictionary(t => (int)_propertyToErrorId[t.propertyName], t => new PossibleError((int)_propertyToErrorId[t.propertyName], t.entry!.Errors[0].ErrorMessage));

        return ErrorState.Create(errors, invalidProperties.Select(t => _propertyToErrorId[t.propertyName]).ToArray());
    }

    protected override IActionResult OnPostWithModel(ConnectionRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            Errors = GetErrors(nameof(AddressLine1), nameof(TownOrCity), nameof(Postcode));
            SetPageProperties(model);
            return Page();
        }

        model.AddressLine1 = AddressLine1;
        model.AddressLine2 = AddressLine2;
        model.TownOrCity = TownOrCity;
        model.County = County;
        model.Postcode = UkGdsPostcodeAttribute.SanitisePostcode(Postcode!);

        return NextPage(ConnectContactDetailsJourneyPage.Letter, model.ContactMethodsSelected);
    }

    private void SetPageProperties(ConnectionRequestModel model)
    {
        HeadingText = $"What is the address for {HttpUtility.HtmlEncode(model.FamilyContactFullName)}?";
        BackUrl = GenerateBackUrl(ConnectContactDetailsJourneyPage.Letter, model.ContactMethodsSelected);
    }
}