// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">分发项类</para>
/// <para lang="en">Dispatch entry class</para>
/// </summary>
/// <typeparam name="TEntry"></typeparam>
public class DispatchEntry<TEntry>
{
    /// <summary>
    /// <para lang="zh">获得/设置 Entry 名称 默认 null</para>
    /// <para lang="en">Get/Set Entry name default null</para>
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Entry 实例 不为空</para>
    /// <para lang="en">Get/Set Entry instance not null</para>
    /// </summary>
    public TEntry? Entry { get; set; }
}
