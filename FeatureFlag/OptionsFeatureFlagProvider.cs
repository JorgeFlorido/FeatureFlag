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
    {
        if (string.IsNullOrWhiteSpace(flagName))
            throw new ArgumentException("Flag name must not be null or empty.", nameof(flagName));

        return Task.FromResult(_options.TryGetValue(flagName, out var value) && value);
    }
  }
}
