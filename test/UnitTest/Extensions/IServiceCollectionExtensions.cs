// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

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
                ["BootstrapBlazorOptions:DefaultCultureInfo"] = cultureName
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

    class MockEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";

        public string ApplicationName { get; set; } = "Test";

        public string ContentRootPath { get; set; } = "UnitTest";

        public IFileProvider ContentRootFileProvider { get; set; } = null!;
    }
}
