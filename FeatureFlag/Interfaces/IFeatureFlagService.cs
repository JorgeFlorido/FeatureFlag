namespace FeatureFlag.Interfaces
{
  public interface IFeatureFlagService
  {
    Task<bool> IsEnabledAsync(string flagName, CancellationToken ct = default);
  }
}
