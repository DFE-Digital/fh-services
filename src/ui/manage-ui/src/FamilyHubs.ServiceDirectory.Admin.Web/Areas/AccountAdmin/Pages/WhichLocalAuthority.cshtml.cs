﻿using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Errors;
using FamilyHubs.ServiceDirectory.Admin.Web.ViewModel;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages;

public class WhichLocalAuthority : AccountAdminViewModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;

    public WhichLocalAuthority(ICacheService cacheService, IServiceDirectoryClient serviceDirectoryClient) : base(nameof(WhichLocalAuthority), cacheService)
    {
        PageHeading = string.Empty;
        _serviceDirectoryClient = serviceDirectoryClient;
        Errors = ErrorState.Empty;
    }
    
    [BindProperty]
    public required string LaOrganisationName { get; set; } = string.Empty;

    public required List<string> LocalAuthorities { get; set; } = new List<string>();

    private const string LaJourneyLabel = "Which local authority do they work for?";
    private const string VcsJourneyLabel = "Which local authority area do they work in?";

    public override async Task OnGet()
    {
        await base.OnGet();
        
        var localAuthorities = await _serviceDirectoryClient.GetCachedLaOrganisations();
        LocalAuthorities = localAuthorities.Select(l => l.Name).ToList();

        LaOrganisationName = PermissionModel.LaOrganisationName;
            
        PageHeading = PermissionModel.VcsJourney ? VcsJourneyLabel : LaJourneyLabel;
    }

    public override async Task<IActionResult> OnPost()
    {
        var laOrganisations = await _serviceDirectoryClient.GetCachedLaOrganisations();
        
        await base.OnPost();

        var organisation = laOrganisations.Find(l => l.Name == LaOrganisationName);

        if (ModelState.IsValid && !string.IsNullOrWhiteSpace(LaOrganisationName) && LaOrganisationName.Length <= 255 && organisation is not null)
        {                        
            PermissionModel.LaOrganisationId = organisation.Id;
            PermissionModel.LaOrganisationName = LaOrganisationName;

            await CacheService.StorePermissionModel(PermissionModel, CacheId);

            return RedirectToPage(NextPageLink, new {cacheId= CacheId});
        }
        
        PageHeading = PermissionModel.VcsJourney ? VcsJourneyLabel : LaJourneyLabel;

        Errors = ErrorState.Create(PossibleErrors.All, ErrorId.AccountAdmin_WhichLocalAuthority_MissingSelection);
        
        LocalAuthorities = laOrganisations.Select(l => l.Name).ToList();

        return Page();
    }
}