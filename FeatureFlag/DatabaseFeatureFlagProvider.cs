using FeatureFlag.Interfaces;

namespace FeatureFlag
{
  public sealed class DatabaseFeatureFlagProvider : IFeatureFlagProvider
  {
    private readonly IFeatureFlagRepository _repository;

    public DatabaseFeatureFlagProvider(IFeatureFlagRepository repository)
    {
      _repository = repository;
    }

    public async Task<bool> IsEnabledAsync(string flagName, CancellationToken ct = default)
    {
      if (string.IsNullOrWhiteSpace(flagName))
        throw new ArgumentException("Flag name must not be null or empty.", nameof(flagName));

      var flag = await _repository.GetFlagAsync(flagName, ct);
      return flag ?? false;
    }
  }
}
