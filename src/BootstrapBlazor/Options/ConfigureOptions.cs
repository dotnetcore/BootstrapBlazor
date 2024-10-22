// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
