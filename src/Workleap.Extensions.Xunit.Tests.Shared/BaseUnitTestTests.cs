using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Workleap.Extensions.Xunit.Tests;

public class BaseUnitTestTests : BaseUnitTest<BaseUnitTestTests.CustomFixture>
{
    public BaseUnitTestTests(CustomFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture, testOutputHelper)
    {
    }

    [Fact]
    public void Fixture_Is_Provided()
    {
        Assert.NotNull(this.Fixture);
    }

    [Fact]
    public void Fixture_Services_Are_Registered()
    {
        Assert.IsType<MyService>(this.Services.GetRequiredService<MyService>());
    }

    [Fact]
    public void Fixture_Configuration_Is_Applied()
    {
        Assert.Equal("bar", this.Services.GetRequiredService<IConfiguration>()["Foo"]);
    }

    [Fact]
    public void Logger_Can_Log()
    {
        Assert.NotNull(this.Logger);
        this.Logger.LogInformation("Hello from {Runtime}", Environment.Version);
    }

    public sealed class MyService
    {
    }

    public sealed class CustomFixture : BaseUnitFixture
    {
        protected override IConfigurationBuilder ConfigureConfiguration(IConfigurationBuilder builder)
        {
            return base.ConfigureConfiguration(builder).AddInMemoryCollection(new Dictionary<string, string?> { ["Foo"] = "bar" });
        }

        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return base.ConfigureServices(services).AddTransient<MyService>();
        }
    }
}
