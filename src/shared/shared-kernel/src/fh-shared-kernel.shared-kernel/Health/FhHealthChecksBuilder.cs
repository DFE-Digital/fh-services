using FamilyHubs.SharedKernel.Health.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FamilyHubs.SharedKernel.Health;

public class FhHealthChecksBuilder
{
    public enum UrlType
    {
        InternalApi,
        ExternalApi,
        ExternalSite
    }

    private readonly IHealthChecksBuilder _builder;
    private readonly IConfiguration _configuration;
    private readonly FhHealthCheckOptions? _fhHealthCheckOptions;
    private readonly Dictionary<string, string>? _urls;

    public FhHealthChecksBuilder(
        IHealthChecksBuilder builder,
        IConfiguration configuration,
        FhHealthCheckOptions? fhHealthCheckOptions,
        Dictionary<string, string>? urls)
    {
        _builder = builder;
        _configuration = configuration;
        _fhHealthCheckOptions = fhHealthCheckOptions;
        _urls = urls;
    }

    public void AddFamilyHubs()
    {
        if (_fhHealthCheckOptions?.Enabled == false)
        {
            return;
        }

        AddUrlTypes(_fhHealthCheckOptions!.InternalApis, UrlType.InternalApi);
        AddUrlTypes(_fhHealthCheckOptions.ExternalApis, UrlType.ExternalApi);
        AddUrlTypes(_fhHealthCheckOptions.ExternalSites, UrlType.ExternalSite);

        AddSqlDatabases(_fhHealthCheckOptions.Databases);
    }

    private void AddSqlDatabases(Dictionary<string, HealthCheckDatabaseOptions> dbs)
    {
        foreach (var db in dbs)
        {
            if (db.Value.ConfigConnectionString != null)
            {
                db.Value.ConnectionString = _configuration[db.Value.ConfigConnectionString];
            }

            if (!string.IsNullOrEmpty(db.Value.ConnectionString))
            {
                _builder.AddSqlServer(
                    db.Value.ConnectionString,
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "Database", db.Key });
            }
        }
    }

    private void ConfigureUrl(HealthCheckUrlOptions link)
    {
        if (link.ConfigUrl != null)
        {
            link.Url = _configuration[link.ConfigUrl];
        }
        else
        {
            // if a base url key is set, treat the Url as a relative url from the given base
            if (!string.IsNullOrEmpty(link.BaseUrlKey))
            {
                link.Url = AddRelativeToBaseUrl(link.BaseUrlKey, link.Url ?? "").ToString();
            }
        }
    }

    //todo: this version doesn't support parent configs
    private Uri AddRelativeToBaseUrl(string baseUrlKeyName, string? relativeUrl = null)
    {
        if (_urls == null)
        {
            throw new ArgumentException("BaseUrl used, but no Urls present");
        }

        if (!_urls.TryGetValue(baseUrlKeyName, out var baseUrlValue))
        {
            throw new ArgumentException($"No path found in Urls for key \"{baseUrlKeyName}\"", baseUrlKeyName);
        }

        return new Uri($"{baseUrlValue.TrimEnd('/')}/{relativeUrl?.TrimStart('/')}");
    }

    private void AddUrlTypes(
        Dictionary<string, HealthCheckUrlOptions> urls,
        UrlType urlType)
    {
        foreach (var url in urls)
        {
            ConfigureUrl(url.Value);
            AddApi(url.Key, url.Value.Url, urlType);
        }
    }

    private void AddApi(string name, string? url, UrlType urlType = UrlType.InternalApi)
    {
        // Only add the health check if the config key is set.
        // Either the API is optional (or not used locally) and missing intentionally,
        // in which case there's no need to add the health check,
        // or it's required, but in that case, the real consumer of the API should
        // continue to throw it's own relevant exception
        if (!string.IsNullOrEmpty(url))
        {
            if (urlType == UrlType.InternalApi)
            {
                //todo: add "/health" endpoints to all APIs
                url = url.TrimEnd('/') + "/api/info";
            }

            // we handle API failures as Degraded, so that App Services doesn't remove or replace the instance (all instances!) due to an API being down
            _builder.AddUrlGroup(new Uri(url), name, HealthStatus.Degraded,
                new[] { urlType.ToString() });
        }
    }
}