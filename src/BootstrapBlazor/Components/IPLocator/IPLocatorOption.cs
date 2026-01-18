// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IP定位器配置项</para>
/// <para lang="en">IP Locator Options</para>
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
public class IPLocatorOption
{
    /// <summary>
    /// <para lang="zh">获得/设置 定位器创建方法未设置使用内部定位器</para>
    /// <para lang="en">Gets or sets Locator Factory Method. Use internal locator if not set</para>
    /// </summary>
    public Func<IServiceProvider, IIPLocator>? LocatorFactory { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 IP地址请求超时时间 默认为 3000 毫秒</para>
    /// <para lang="en">Gets or sets IP Address Request Timeout. Default 3000 ms</para>
    /// </summary>
    public int RequestTimeout { get; set; } = 3000;

    /// <summary>
    /// <para lang="zh">获得 本机地址列表</para>
    /// <para lang="en">Get Localhost Address List</para>
    /// </summary>
    public List<string> Localhosts { get; } = new List<string>(new string[] { "::1", "127.0.0.1" });

    /// <summary>
    /// <para lang="zh">获得/设置 IP地址</para>
    /// <para lang="en">Gets or sets IP Address</para>
    /// </summary>
    protected internal string? IP { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 HttpClient 实体类</para>
    /// <para lang="en">Gets or sets HttpClient Instance</para>
    /// </summary>
    protected internal HttpClient? HttpClient { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ILogger 实体类</para>
    /// <para lang="en">Gets or sets ILogger Instance</para>
    /// </summary>
    protected internal ILogger<IIPLocatorProvider>? Logger { get; set; }
}
