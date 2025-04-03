using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;

namespace FamilyHubs.SharedKernel.Razor.UnitTests.FamilyHubsUi.Configure.Helpers;

public class FamilyHubsUiOptionsTestBase
{
    public FamilyHubsUiOptions FamilyHubsUiOptions { get; set; }

    public FamilyHubsUiOptionsTestBase()
    {
        FamilyHubsUiOptions = new FamilyHubsUiOptions
        {
            ServiceName = "ServiceName",
            Phase = Phase.Alpha,
            FeedbackUrl = "FeedbackUrl",
            Analytics = new AnalyticsOptions
            {
                CookieName = "CookieName",
                MeasurementId = "MeasurementId",
                ClarityId = "ClarityId",
                ContainerId = "ContainerId"
            },
            Header = new HeaderOptions
            {
                NavigationLinks = new[]
                {
                    new FhLinkOptions
                    {
                        Text = "header navigation link",
                        Url = "https://example.com/navigation",
                        ConfigUrl = null
                    }
                },
                ActionLinks = new[]
                {
                    new FhLinkOptions
                    {
                        Text = "header action link",
                        Url = "https://example.com/action",
                        ConfigUrl = null
                    }
                }
            },
            Footer = new FooterOptions
            {
                Links = new[]
                {
                    new FhLinkOptions
                    {
                        Text = "Text",
                        Url = "text",
                        ConfigUrl = null
                    }
                }
            }
        };
    }
}