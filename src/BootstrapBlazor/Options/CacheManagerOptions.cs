// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">CacheManagerOptions 配置类</para>
/// <para lang="en">CacheManagerOptions configuration class</para>
/// </summary>
public class CacheManagerOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否开启 CacheManager 功能 默认 true 开启</para>
    /// <para lang="en">Get/Set whether to enable CacheManager feature default true</para>
    /// </summary>
    public bool Enable { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 滑动缓存过期时间 默认 5 分钟</para>
    /// <para lang="en">Get/Set sliding expiration time default 5 minutes</para>
    /// </summary>
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// <para lang="zh">获得/设置 绝对缓存过期时间 默认 10 秒钟</para>
    /// <para lang="en">Get/Set absolute expiration time default 10 seconds</para>
    /// </summary>
    public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromSeconds(10);
}
