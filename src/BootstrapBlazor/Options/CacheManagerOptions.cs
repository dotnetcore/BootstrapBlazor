// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// CacheManagerOptions 配置类
/// </summary>
public class CacheManagerOptions
{
    /// <summary>
    /// 获得/设置 是否开启 CacheManager 功能 默认 true 开启
    /// </summary>
    public bool Enable { get; set; } = true;

    /// <summary>
    /// 获得/设置 滑动缓存过期时间 默认 5 分钟
    /// </summary>
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// 获得/设置 绝对缓存过期时间 默认 10 秒钟
    /// </summary>
    public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromSeconds(10);
}
