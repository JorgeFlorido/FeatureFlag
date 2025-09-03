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

    public Task<bool> IsEnabledAsync(string flagName, CancellationToken cancellationToken = default)
    {
      if (string.IsNullOrWhiteSpace(flagName))
        throw new ArgumentException("Flag name must not be null or empty.", nameof(flagName));

      return Task.FromResult(_flags.TryGetValue(flagName, out var value) && value);
    }
  }
}
