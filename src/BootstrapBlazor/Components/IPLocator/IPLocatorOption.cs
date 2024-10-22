﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// IP定位器配置项
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
public class IPLocatorOption
{
    /// <summary>
    /// 获得/设置 定位器创建方法未设置使用内部定位器
    /// </summary>
    public Func<IServiceProvider, IIPLocator>? LocatorFactory { get; set; }

    /// <summary>
    /// 获得/设置 IP地址请求超时时间 默认为 3000 毫秒
    /// </summary>
    public int RequestTimeout { get; set; } = 3000;

    /// <summary>
    /// 获得 本机地址列表
    /// </summary>
    public List<string> Localhosts { get; } = new List<string>(new string[] { "::1", "127.0.0.1" });

    /// <summary>
    /// 获得/设置 IP地址
    /// </summary>
    protected internal string? IP { get; set; }

    /// <summary>
    /// 获得/设置 HttpClient 实体类
    /// </summary>
    protected internal HttpClient? HttpClient { get; set; }

    /// <summary>
    /// 获得/设置 ILogger 实体类
    /// </summary>
    protected internal ILogger<IIPLocatorProvider>? Logger { get; set; }
}
