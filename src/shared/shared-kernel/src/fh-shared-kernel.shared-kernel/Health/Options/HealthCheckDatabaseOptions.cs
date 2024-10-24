namespace FamilyHubs.SharedKernel.Health.Options;

public class HealthCheckDatabaseOptions
{
    public enum DatabaseType
    {
        SqlServer
    }

    /// <summary>
    /// The ConnectionString.
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// If supplied, the ConnectionString is populated from the config value found at the given config key.
    /// </summary>
    /// <example>
    /// "ConnectionStrings:SharedKernelConnection"
    /// </example>
    public string? ConfigConnectionString { get; set; }

    public DatabaseType Type { get; set; } = DatabaseType.SqlServer;
}