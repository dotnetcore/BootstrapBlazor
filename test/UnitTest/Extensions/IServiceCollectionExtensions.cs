// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
