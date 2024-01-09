// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BlazorConfigureFromConfigurationOptions 配置实现类
/// </summary>
public class ConfigureOptions<TOption> : ConfigureFromConfigurationOptions<TOption> where TOption : class
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="config"></param>
    public ConfigureOptions(IConfiguration config)
        : base(config.GetSection(typeof(TOption).Name))
    {

    }
}
