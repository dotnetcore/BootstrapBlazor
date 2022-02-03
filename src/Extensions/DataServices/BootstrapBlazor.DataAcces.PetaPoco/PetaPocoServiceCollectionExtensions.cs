// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.DataAcces.PetaPoco;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PetaPoco;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class PetaPocoServiceCollectionExtensions
{
    /// <summary>
    /// 增加 PetaPoco 数据库操作服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddPetaPoco(this IServiceCollection services, Action<IDatabaseBuildConfiguration> optionsAction)
    {
        services.AddTransient<IDatabase>(sp =>
        {
            var builder = DatabaseConfiguration.Build();
            optionsAction(builder);
            return new Database(builder);
        });
        services.TryAddSingleton(typeof(IDataService<>), typeof(DefaultDataService<>));
        return services;
    }
}
