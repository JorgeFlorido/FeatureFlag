# Feature Flags Library

A lightweight .NET library providing a clean abstraction for feature flags with multiple backends.

## Supported Providers

- **InMemoryFeatureFlagProvider** – for local testing and unit tests.
- **JsonFileFeatureFlagProvider** – reads feature flags from a JSON configuration file.
- **DatabaseFeatureFlagProvider** – retrieves feature flags from a database via a repository.
- **LaunchDarklyFeatureFlagProvider** – integrates with [LaunchDarkly](https://launchdarkly.com).

## Usage

### 1. Register in `Program.cs`

```csharp
builder.Services.AddSingleton<IFeatureFlagProvider>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var sdkKey = config["LaunchDarkly:SdkKey"];
    return new LaunchDarklyFeatureFlagProvider(sdkKey);
});
