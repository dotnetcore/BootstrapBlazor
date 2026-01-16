// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///  <para lang="zh">BlazorConfigureFromConfigurationOptions 配置实现类</para>
///  <para lang="en">BlazorConfigureFromConfigurationOptions configuration implementation class</para>
/// </summary>
public class ConfigureOptions<TOption> : ConfigureFromConfigurationOptions<TOption> where TOption : class
{
    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="config"></param>
    public ConfigureOptions(IConfiguration config)
        : base(config.GetSection(typeof(TOption).Name))
    {

    }
}
