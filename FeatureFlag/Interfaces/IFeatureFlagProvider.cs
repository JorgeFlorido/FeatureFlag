namespace FeatureFlag.Interfaces
{
  public interface IFeatureFlagProvider
  {
    Task<bool> IsEnabledAsync(string flagName, CancellationToken cancellationToken = default);
  }
}
