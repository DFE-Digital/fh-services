using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace FamilyHubs.SharedKernel.Razor.FeatureFlags;

[FilterAlias(nameof(AlsoFilter))]
public class AlsoFilter : IFeatureFilter
{
    private readonly Lazy<IFeatureManager> _featureManager;

    public AlsoFilter(IServiceProvider serviceProvider)
    {
        _featureManager = new Lazy<IFeatureManager>(serviceProvider.GetRequiredService<IFeatureManager>);
    }

    public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var settings = context.Parameters.Get<AlsoFilterFilterSettings>();

        var flags = settings?.Flags?.Split(",") ?? [];
        var featureStates = flags.Select(flagName => _featureManager.Value.IsEnabledAsync(flagName)).ToArray();

        return Array.Exists(
            await Task.WhenAll(featureStates),
            x => x
        );
    }

    public class AlsoFilterFilterSettings
    {
        public string? Flags { get; init; }
    }
}
