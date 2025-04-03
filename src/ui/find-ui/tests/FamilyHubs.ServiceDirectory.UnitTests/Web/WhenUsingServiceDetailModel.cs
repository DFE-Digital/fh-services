using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using ServiceDetailModel = FamilyHubs.ServiceDirectory.Web.Pages.ServiceDetail.Index;

namespace FamilyHubs.ServiceDirectory.UnitTests.Web;

public class WhenUsingServiceDetailModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();

    private readonly ServiceDetailModel _serviceDetailModel;

    public WhenUsingServiceDetailModel()
    {
        _serviceDetailModel = new ServiceDetailModel(
            _serviceDirectoryClient
        );
    }

    [Fact]
    public async Task LoadIndex()
    {
        var expected = new ServiceDirectory.Web.Models.ServiceDetail.ServiceDetailModel
        {
            Name = "ExampleService",
            Schedule = new Schedule
            {
                DaysAvailable = "Saturday, Sunday",
                ExtraAvailabilityDetails = "ServiceScheduleDescription"
            },
            Categories = ["A", "B", "C"],
            Contact = new Contact
            {
                Email = "email@example.com",
                Phone = "01234567890",
                TextMessage = "02233445566",
                Website = "example.com"
            },
            Eligibility = "Yes, 18 to 65 years old",
            Languages = ["English"],
            AttendingTypes = "Telephone",
            Locations = [
                new Location
                {
                    IsFamilyHub = "No",
                    Schedule = new Schedule
                    {
                        DaysAvailable = "Tuesday, Thursday",
                        ExtraAvailabilityDetails = "ServiceAtLocationsScheduleDescription"
                    },
                    Accessibilities = ["Test Accessibility"],
                    Address = ["ExampleAddress", "ExampleCity", "ExampleStateProvince", "ExamplePostCode"],
                    Details = "LocationDescription"
                }
            ],
            MoreDetails = "ServiceDescription",
            OnlineTelephone = "Telephone",
            Cost = "Free",
            Summary = null
        };

        _serviceDirectoryClient.GetServiceById(Arg.Any<long>(), Arg.Any<CancellationToken>())
            .Returns(TestData.ExampleServices.First());

        // Act
        var result = await _serviceDetailModel.OnGetAsync(1, "/ServiceFilter");

        // Assert
        Assert.IsType<PageResult>(result);
        
        Assert.Equal(expected, _serviceDetailModel.Service);
    }
    
    [Fact]
    public async Task ShouldHaveHas_ContactDetailsTrue_IfAnyContactDetailsArePresent()
    {
        _serviceDirectoryClient.GetServiceById(Arg.Any<long>(), Arg.Any<CancellationToken>())
            .Returns(TestData.ExampleServices.First());

        // Act
        await _serviceDetailModel.OnGetAsync(1, "/ServiceFilter");

        // Assert
        Assert.True(_serviceDetailModel.HasContactDetails);
    }

    [Fact]
    public async Task FailsOnInvalidReturnUrl()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _serviceDetailModel.OnGetAsync(1, "https://mybadsite.com/MaliciousPage"));
    }
}
