using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Models.ServiceJourney;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using FamilyHubs.ServiceDirectory.Shared.ReferenceData;
using FamilyHubs.SharedKernel.Razor.AddAnother;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

public class WhatLanguageViewModel
{
    public IEnumerable<string> Languages { get; set; } = [];
    public bool TranslationServices { get; set; }
    public bool BritishSignLanguage { get; set; }
    public AddAnotherAutocompleteErrorChecker? ErrorIndexes { get; set; }
}

public class What_LanguageModel : ServicePageModel<WhatLanguageViewModel>
{
    public const string NoLanguageValue = "";
    public const string NoLanguageText = "";
    public const string InvalidNameValue = "--";

    public static SelectListItem[] LanguageOptions { get; set; }

    static What_LanguageModel()
    {
        LanguageOptions = Languages.CodeToName
            .OrderBy(kv => kv.Value)
            .Select(kv => new SelectListItem(kv.Value, kv.Key))
            .Prepend(new SelectListItem(NoLanguageText, NoLanguageValue, true, true))
            .ToArray();
    }

    public IEnumerable<SelectListItem> UserLanguageOptions { get; set; } = [];

    [BindProperty]
    public bool TranslationServices { get; set; }
    [BindProperty]
    public bool BritishSignLanguage { get; set; }

    public Dictionary<int, int>? ErrorIdToFirstSelectIndex { get; set; }
    public Dictionary<int, SharedKernel.Razor.ErrorNext.Error>? SelectIndexToError { get; set; }

    public What_LanguageModel(IRequestDistributedCache connectionRequestCache)
        : base(ServiceJourneyPage.What_Language, connectionRequestCache)
    {
    }

    protected override void OnGetWithError()
    {
        SetFormUserInputData();

        if (ServiceModel?.UserInput?.ErrorIndexes == null)
        {
            throw new InvalidOperationException("ServiceModel?.UserInput?.ErrorIndexes is null");
        }

        ErrorIdToFirstSelectIndex = new Dictionary<int, int>();
        SelectIndexToError = new Dictionary<int, SharedKernel.Razor.ErrorNext.Error>();

        var errorIndexes = ServiceModel.UserInput.ErrorIndexes;

        AddToErrorLookups(ErrorId.What_Language__EnterLanguages, errorIndexes.EmptyIndexes);
        AddToErrorLookups(ErrorId.What_Language__EnterSupportedLanguage, errorIndexes.InvalidIndexes);
        AddDuplicatesToErrorLookups(ErrorId.What_Language__SelectLanguageOnce, errorIndexes.DuplicateIndexes);
    }

    protected override void OnGetWithModel()
    {
        // redirectingToSelf is only set when adding a new field. Javascript is disabled
        if(ServiceModel?.UserInput is not null && Request.Query.TryGetValue("redirectingToSelf", out var redirectToSelf) && redirectToSelf == "true")
        {
            SetFormUserInputData();
            return;
        }

        SetFormServiceModelData();
    }
    
    protected override IActionResult OnPostWithModel()
    {
        //todo: do we want to split the calls in base to have OnPostErrorChecksAsync and OnPostUpdateAsync? (or something)

        IEnumerable<string> languageCodes = Request.Form["language"];
        var viewModel = new WhatLanguageViewModel
        {
            Languages = Request.Form["languageName"],
            TranslationServices = TranslationServices,
            BritishSignLanguage = BritishSignLanguage
        };

        // handle add/remove buttons first. if there are any validation errors, we'll ignore then until they click continue
        var button = Request.Form["button"].FirstOrDefault();

        if (button != null)
        {
            // to get here, the user must have javascript disabled
            // the form contains the select values in "language" and there are no "languageName" values as the inputs weren't created

            if (button is "add")
            {
                //todo: if javascript is disabled, we *could* keep a count the number of empty language inputs, or have a different name for each select with each having a hidden field
                // but it's a lot of effort for probably little or no users
                languageCodes = languageCodes.Append(NoLanguageValue);
            }
            else if (button.StartsWith("remove"))
            {
                var indexToRemove = int.Parse(button.Substring("remove-".Length));
                if (indexToRemove < languageCodes.Count())
                {
                    languageCodes = languageCodes.Where((_, i) => i != indexToRemove);

                    if (!languageCodes.Any())
                    {
                        languageCodes = languageCodes.Append(NoLanguageValue);
                    }
                }
            }

            viewModel.Languages = languageCodes
                .Select(c => c == NoLanguageValue ? NoLanguageValue : Languages.CodeToName[c]);

            return RedirectToSelf(viewModel);
        }

        //todo: find all instances, rather than just first?
        viewModel.ErrorIndexes = AddAnotherAutocompleteErrorChecker.Create(
            Request.Form, "language", "languageName", LanguageOptions.Skip(1));

        var errorIds = new List<ErrorId>();
        if (viewModel.ErrorIndexes.EmptyIndexes.Any())
        {
            errorIds.Add(ErrorId.What_Language__EnterLanguages);
        }
        if (viewModel.ErrorIndexes.InvalidIndexes.Any())
        {
            errorIds.Add(ErrorId.What_Language__EnterSupportedLanguage);
        }
        if (viewModel.ErrorIndexes.DuplicateIndexes.Any())
        {
            errorIds.Add(ErrorId.What_Language__SelectLanguageOnce);
        }

        if (errorIds.Count > 0)
        {
            if (!viewModel.Languages.Any())
            {
                // handle the case where javascript is disabled and the user has a single empty select
                viewModel.Languages = viewModel.Languages.Append(NoLanguageText);
            }
            return RedirectToSelf(viewModel, errorIds.ToArray());
        }

        ServiceModel!.Updated = ServiceModel.Updated || HaveLanguagesBeenUpdated(languageCodes);

        ServiceModel!.LanguageCodes = languageCodes;
        ServiceModel.TranslationServices = TranslationServices;
        ServiceModel.BritishSignLanguage = BritishSignLanguage;

        return NextPage();
    }

    private void SetFormServiceModelData()
    {
        SetServiceModelLanguageOptions();
        
        TranslationServices = ServiceModel!.TranslationServices ?? false;
        BritishSignLanguage = ServiceModel!.BritishSignLanguage ?? false;
        
    }

    private void SetFormUserInputData()
    {
        // Override with the languages that are already selected
        SetUserInputLanguageOptions();

        TranslationServices = ServiceModel!.UserInput?.TranslationServices ?? false;
        BritishSignLanguage = ServiceModel!.UserInput?.BritishSignLanguage ?? false;
    }
    
    private void SetServiceModelLanguageOptions()
    {
        // default to no language selected
        UserLanguageOptions = LanguageOptions.Take(1);
        if (ServiceModel!.LanguageCodes?.Any() == true)
        {
            UserLanguageOptions = ServiceModel!.LanguageCodes.Select(lang =>
            {
                //todo: put into method
                var codeFound = Languages.CodeToName.TryGetValue(lang, out var name);
                return new SelectListItem(name, codeFound ? lang : InvalidNameValue);
            });
        }
        UserLanguageOptions = UserLanguageOptions.OrderBy(sli => sli.Text);
    }

    private void SetUserInputLanguageOptions()
    {
        UserLanguageOptions =  ServiceModel!.UserInput!.Languages
            .Select(name =>
            {
                if (name == NoLanguageText)
                {
                    return new SelectListItem(NoLanguageText, NoLanguageValue);
                }

                var nameFound = Languages.NameToCode.TryGetValue(name, out var code);
                return new SelectListItem(name, nameFound ? code : InvalidNameValue);
            });
    }

    private void AddToErrorLookups(ErrorId errorId, IEnumerable<int> indexes)
    {
        var error = Errors.GetErrorIfTriggered((int)errorId);
        if (error == null)
        {
            return;
        }

        ErrorIdToFirstSelectIndex!.Add(error.Id, indexes.First());
        foreach (var index in indexes)
        {
            SelectIndexToError!.Add(index, error);
        }
    }

    private void AddDuplicatesToErrorLookups(ErrorId errorId, IEnumerable<IEnumerable<int>> setIndexes)
    {
        var error = Errors.GetErrorIfTriggered((int)errorId);
        if (error == null)
        {
            return;
        }

        ErrorIdToFirstSelectIndex!.Add(error.Id,
            setIndexes.SelectMany(si => si.Skip(1).Take(1)).Min());

        foreach (var indexes in setIndexes)
        {
            foreach (var index in indexes.Skip(1))
            {
                SelectIndexToError!.Add(index, error);
            }
        }
    }

    

    // updated only *needs* to be set if in edit flow. do we want to check?
    private bool HaveLanguagesBeenUpdated(IEnumerable<string> languageCodes)
    {
        var languagesAreEqual = ServiceModel!.LanguageCodes != null && 
                                ServiceModel.LanguageCodes
                                    .OrderBy(x => x)
                                    .SequenceEqual(languageCodes.OrderBy(x => x));

        return !languagesAreEqual
               || ServiceModel.TranslationServices != TranslationServices
               || ServiceModel.BritishSignLanguage != BritishSignLanguage;
    }
}