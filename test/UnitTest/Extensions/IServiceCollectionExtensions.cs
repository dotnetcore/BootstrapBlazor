// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;

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
}
