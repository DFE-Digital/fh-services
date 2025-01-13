using System.Text.Json;
using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models.ServiceJourney;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;
using Microsoft.AspNetCore.Mvc.Rendering;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Pages.manage_services;

public class WhenWhatLanguageOnGetIsCalled
{
    private readonly IRequestDistributedCache _mockCache;

    public WhenWhatLanguageOnGetIsCalled()
    {
        _mockCache = Substitute.For<IRequestDistributedCache>();
    }
    
    [Fact]
    public async Task OnGetWithModel_NoUserInputOrPreviousEntry_PopulatesUserLanguageOptionsWithDefault()
    {
        // Arrange
        var cachedData = new ServiceModel<WhatLanguageViewModel>();
        _mockCache.GetAsync<ServiceModel<WhatLanguageViewModel>>(Arg.Any<string>()).Returns(cachedData);

        var model = new What_LanguageModel(_mockCache);

        // Act
        await model.OnGetAsync("add", null, false);

        // Assert
        var expectedOptions = new List<SelectListItem>
        {
            new("", ""),
        };

        Assert.Equal(expectedOptions.Select(o => o.Value), model.UserLanguageOptions.Select(o => o.Value));
        Assert.Equal(expectedOptions.Select(o => o.Text), model.UserLanguageOptions.Select(o => o.Text));
    }
    
    [Fact]
    public async Task OnGetWithModel_NoUserInputWithPreviousEntry_PopulatesUserLanguageOptionsFromServiceModel()
    {
        // Arrange
        var cachedData = new ServiceModel<WhatLanguageViewModel>
        {
            LanguageCodes = new List<string> { "en", "fr" }
        };
        _mockCache.GetAsync<ServiceModel<WhatLanguageViewModel>>(Arg.Any<string>()).Returns(cachedData);

        var model = new What_LanguageModel(_mockCache);

        // Act
        await model.OnGetAsync("add", null, false);

        // Assert
        var expectedOptions = new List<SelectListItem>
        {
            new("English", "en"),
            new("French", "fr")
        };

        Assert.Equal(expectedOptions.Select(o => o.Value), model.UserLanguageOptions.Select(o => o.Value));
        Assert.Equal(expectedOptions.Select(o => o.Text), model.UserLanguageOptions.Select(o => o.Text));
    }

    [Fact]
    public async Task OnGetWithModel_RedirectingToSelf_PopulatesUserLanguageOptionsFromUserInput()
    {
        // Arrange
        var mockVm = new WhatLanguageViewModel
        {
            Languages = ["English", "French"]
        };
        var cachedData = new ServiceModel<WhatLanguageViewModel>
        {
            UserInput = mockVm,
            UserInputType = typeof(WhatLanguageViewModel).FullName,
            UserInputJson = JsonSerializer.Serialize(mockVm),
            LanguageCodes = new List<string> { "de" } // <- Here to prove it only gets values from UserInput
        };
        _mockCache.GetAsync<ServiceModel<WhatLanguageViewModel>>(Arg.Any<string>()).Returns(cachedData);

        var model = new What_LanguageModel(_mockCache);

        // Act
        await model.OnGetAsync("add", null, true);

        // Assert
        var expectedOptions = new List<SelectListItem>
        {
            new("English", "en"),
            new("French", "fr")
        };

        Assert.Equal(expectedOptions.Select(o => o.Value), model.UserLanguageOptions.Select(o => o.Value));
        Assert.Equal(expectedOptions.Select(o => o.Text), model.UserLanguageOptions.Select(o => o.Text));
    }
}