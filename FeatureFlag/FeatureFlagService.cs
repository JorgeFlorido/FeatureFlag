using FeatureFlag.Interfaces;

namespace FeatureFlag
{
  public sealed class FeatureFlagService : IFeatureFlagService
  {
    private readonly IFeatureFlagProvider _provider;

    public FeatureFlagService(IFeatureFlagProvider provider)
        => _provider = provider;

    public Task<bool> IsEnabledAsync(string flagName, CancellationToken ct = default)
        => _provider.IsEnabledAsync(flagName, ct);
  }
}