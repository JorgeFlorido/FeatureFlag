using FeatureFlag.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FeatureFlag.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddFeatureFlags(this IServiceCollection services, IConfiguration configuration, IDictionary<string, bool>? initialFlags = null)
    {
      var providerType = configuration["FeatureFlags:Provider"];

      switch (providerType)
      {
        case "InMemory":
          services.AddSingleton<IFeatureFlagProvider>(_ =>
          {
            var flags = initialFlags ?? new Dictionary<string, bool>();
            return new InMemoryFeatureFlagProvider(flags);
          });
          break;

        case "Database":
          services.AddSingleton<IFeatureFlagProvider>(sp =>
          {
            var repo = sp.GetRequiredService<IFeatureFlagRepository>();
            return new DatabaseFeatureFlagProvider(repo);
          });
          break;

        case "LaunchDarkly":
          services.AddSingleton<IFeatureFlagProvider>(sp =>
          {
            var sdkKey = configuration["LaunchDarkly:SdkKey"]
                         ?? throw new InvalidOperationException("LaunchDarkly SDK key must be provided.");
            var provider = new LaunchDarklyFeatureFlagProvider(sdkKey);
            
            var lifetime = sp.GetRequiredService<IHostApplicationLifetime>();
            lifetime.ApplicationStopping.Register(() => provider.DisposeAsync().AsTask().Wait());
            
            return provider;
          });
          break;

        default:
          throw new InvalidOperationException($"Unsupported FeatureFlag provider: {providerType}");
      }

      services.AddSingleton<IFeatureFlagService, FeatureFlagService>();
      
      return services;
    }
  }
}
