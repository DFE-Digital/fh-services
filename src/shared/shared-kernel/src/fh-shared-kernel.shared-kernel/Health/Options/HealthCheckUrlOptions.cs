namespace FamilyHubs.SharedKernel.Health.Options;

public class HealthCheckUrlOptions
{
    /// <summary>
    /// A key name from the FamilyHubsUi:Url section.
    /// If supplied, the configured value of the given key name,
    /// acts as the base URL for the relative Url supplied through Url or ConfigUrl.
    /// </summary>
    public string? BaseUrlKey { get; set; }

    /// <summary>
    /// The URL for the link.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// If supplied, the Url is populated from the config value found at the given config key.
    /// </summary>
    /// <example>
    /// "FamilyHubsUi:FeedbackUrl"
    /// </example>
    public string? ConfigUrl { get; set; }
}