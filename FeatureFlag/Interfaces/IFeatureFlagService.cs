namespace FeatureFlag.Interfaces
{
  public interface IFeatureFlagService
  {
    bool IsEnabled(string flagName);
    Task<bool> IsEnabledAsync(string flagName, CancellationToken ct = default);
  }
}
