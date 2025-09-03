using FeatureFlag.Interfaces;
using System.Collections.Concurrent;

namespace FeatureFlag
{
  public sealed class InMemoryFeatureFlagProvider : IFeatureFlagProvider
  {

    private readonly ConcurrentDictionary<string, bool> _flags;

    public InMemoryFeatureFlagProvider(IDictionary<string, bool> initialFlags)
    {
      _flags = new ConcurrentDictionary<string, bool>(initialFlags);
    }

    public bool IsEnabled(string flagName)
    {
      return _flags.TryGetValue(flagName, out var value) && value;
    }

    public Task<bool> IsEnabledAsync(string flagName, CancellationToken cancellationToken = default)
    {
      return Task.FromResult(IsEnabled(flagName));
    }
  }
}
