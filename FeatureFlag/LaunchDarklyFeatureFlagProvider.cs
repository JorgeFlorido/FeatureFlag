using FeatureFlag.Interfaces;
using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;

namespace FeatureFlag
{
  public sealed class LaunchDarklyFeatureFlagProvider : IFeatureFlagProvider, IAsyncDisposable
  {
    private readonly LdClient _client;

    public LaunchDarklyFeatureFlagProvider(string sdkKey)
    {
      if (string.IsNullOrWhiteSpace(sdkKey))
        throw new ArgumentException("LaunchDarkly SDK key must not be null or empty.", nameof(sdkKey));

      _client = new LdClient(sdkKey);
    }

    public Task<bool> IsEnabledAsync(string flagName, CancellationToken cancellationToken = default)
    {
      if (string.IsNullOrWhiteSpace(flagName))
        throw new ArgumentException("Flag name must not be null or empty.", nameof(flagName));

      var context = Context.Builder("system-user").Build();
      var result = _client.BoolVariation(flagName, context, false);
      return Task.FromResult(result);
    }

    public ValueTask DisposeAsync()
    {
      _client.Dispose();
      return ValueTask.CompletedTask;
    }
  }
}
