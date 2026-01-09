// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, string? cultureName = null)
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        if (cultureName != null)
        {
            builder.AddInMemoryCollection(new Dictionary<string, string?>()
            {
                ["BootstrapBlazorOptions:DefaultCultureInfo"] = cultureName,
                ["Logging:LogLevel:Default"] = "Information"
            });
        }
        var config = builder.Build();
        services.AddSingleton<IConfiguration>(config);
        return services;
    }

    public static IServiceCollection AddMockEnvironment(this IServiceCollection services)
    {
        services.AddSingleton<IHostEnvironment, MockEnvironment>();
        return services;
    }

    public static ILoggingBuilder AddMockLoggerProvider(this ILoggingBuilder builder)
    {
        return builder.AddProvider(new TestLoggerProvider());
    }

    class TestLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new TestLogger();

        public void Dispose() { }
    }

    class TestLogger : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

        }
    }

    class MockEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";

        public string ApplicationName { get; set; } = "Test";

        public string ContentRootPath { get; set; } = "UnitTest";

        public IFileProvider ContentRootFileProvider { get; set; } = null!;
    }
}
