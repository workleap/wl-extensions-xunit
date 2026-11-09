using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Workleap.Extensions.Xunit.Tests;

public class BaseIntegrationTestTests : BaseIntegrationTest
{
    public BaseIntegrationTestTests(EmptyIntegrationFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture, testOutputHelper)
    {
    }

    [Fact]
    public void HostEnvironment_Is_Registered()
    {
        var env = this.Services.GetRequiredService<IHostEnvironment>();
        var expected = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") is { Length: > 0 } name
            ? name
            : Environment.GetEnvironmentVariable("GITHUB_ACTIONS") is { Length: > 0 } ? "Test" : "Local";

        Assert.Equal(expected, env.EnvironmentName);
    }
}
