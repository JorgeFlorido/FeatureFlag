using FeatureFlag.Interfaces;
using Microsoft.Extensions.Options;

namespace FeatureFlag
{
  public sealed class OptionsFeatureFlagProvider : IFeatureFlagProvider
  {
    private readonly IDictionary<string, bool> _options;

    public OptionsFeatureFlagProvider(IDictionary<string, bool> options)
        => _options = options;

    public Task<bool> IsEnabledAsync(string flagName, CancellationToken ct = default)
        => Task.FromResult(_options.TryGetValue(flagName, out var value) && value);
  }
}
