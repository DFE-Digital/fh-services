using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace FamilyHubs.SharedKernel.Razor.FeatureFlags;

[FilterAlias(nameof(EnabledFilter))]
public class EnabledFilter : IFeatureFilter
{
    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var settings = context.Parameters.Get<EnabledFilterSettings>();
        return Task.FromResult(settings?.Value == true);
    }

    public class EnabledFilterSettings
    {
        public bool Value { get; init; }
    }
}
