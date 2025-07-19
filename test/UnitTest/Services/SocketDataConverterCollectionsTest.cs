// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UnitTest.Services;

public class SocketDataConverterCollectionsTest : BootstrapBlazorTestBase
{
    protected override void ConfigureConfiguration(IServiceCollection services)
    {
        base.ConfigureConfiguration(services);

        services.ConfigureSocketDataConverters(options =>
        {
            options.Add(new SocketDataConverter<MockEntity>());
        });
    }

    [Fact]
    public void Test_Ok()
    {
        var service = Context.Services.GetRequiredService<IOptions<SocketDataConverterCollections>>();
        Assert.NotNull(service.Value);

        var converter = service.Value.GetConverter<MockEntity>();
        Assert.NotNull(converter);

        var fakeConverter = service.Value.GetConverter<Foo>();
        Assert.Null(fakeConverter);
    }

    class MockEntity
    {
        public byte[]? Header { get; set; }

        public byte[]? Body { get; set; }
    }

    class MockLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MockLogger();
        }

        public void Dispose()
        {

        }
    }

    class MockLogger : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

        }
    }
}
