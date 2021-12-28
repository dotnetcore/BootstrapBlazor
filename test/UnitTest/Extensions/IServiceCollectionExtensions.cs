// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, string? cultureName = null)
    {
        var builder = new ConfigurationBuilder();
        var dirSeparator = Path.DirectorySeparatorChar;
        var file = Path.Combine(AppContext.BaseDirectory, $"..{dirSeparator}..{dirSeparator}..{dirSeparator}appsettings.json");
        builder.AddJsonFile(file, true, true);
        if (cultureName != null)
        {
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                ["BootstrapBlazorOptions:DefaultCultureInfo"] = cultureName
            });
        }
        var config = builder.Build();
        services.AddSingleton<IConfiguration>(config);
        return services;
    }
}
