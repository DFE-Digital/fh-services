using System.Text.Json;
using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models.ServiceJourney;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Pages.manage_services;

public class WhenWhatLanguageOnPostIsCalled
{
    private readonly IRequestDistributedCache _mockCache;

    public WhenWhatLanguageOnPostIsCalled()
    {
        _mockCache = Substitute.For<IRequestDistributedCache>();
    }
    
    [Fact]
    public async Task OnPostWithModel_RemoveButton_RemovesLanguage()
    {
        // Arrange
        var cachedData = new ServiceModel<WhatLanguageViewModel>();
        var model = new What_LanguageModel(_mockCache)
        {
            ServiceModel = cachedData
        };
        _mockCache.GetAsync<ServiceModel<WhatLanguageViewModel>>(Arg.Any<string>()).Returns(cachedData);

        var formCollection = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
        {
            { "button", "remove-1" }, // Simulate remove button click for the second language
            { "language", new Microsoft.Extensions.Primitives.StringValues(["en", "fr", "de"]) }
        });

        var httpContext = new DefaultHttpContext
        {
            Request = { Form = formCollection }
        };

        model.PageContext.HttpContext = httpContext;

        // Act
        await model.OnPostAsync("add", null, default);

        // Assert
        // Necessary deserialization of UserInputJson as the model does not retain the UserInput
        var userInput = JsonSerializer.Deserialize<WhatLanguageViewModel>(model.ServiceModel.UserInputJson!);
        var updatedLanguages = userInput!.Languages.ToList();
        Assert.NotNull(updatedLanguages);
        Assert.DoesNotContain("fr", updatedLanguages);
        Assert.Equal(2, updatedLanguages!.Count);
    }
}