using FeatureFlag.Interfaces;
using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;

namespace FeatureFlag
{
  public sealed class LaunchDarklyFeatureFlagProvider : IFeatureFlagProvider, IDisposable
  {
    private readonly LdClient _client;

    public LaunchDarklyFeatureFlagProvider(string sdkKey)
    {
      _client = new LdClient(sdkKey);
    }

    public bool IsEnabled(string flagName) =>
        _client.BoolVariation(flagName, Context.New("system"), false);

    public Task<bool> IsEnabledAsync(string flagName, CancellationToken ct = default) =>
        Task.FromResult(IsEnabled(flagName));

    public void Dispose() => _client.Dispose();
  }
}
