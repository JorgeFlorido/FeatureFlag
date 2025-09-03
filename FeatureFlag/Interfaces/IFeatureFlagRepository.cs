namespace FeatureFlag.Interfaces
{
  public interface IFeatureFlagRepository
  {
    Task<bool?> GetFlagAsync(string name, CancellationToken ct = default);
  }
}
