// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 分发项类
/// </summary>
/// <typeparam name="TEntry"></typeparam>
public class DispatchEntry<TEntry>
{
    /// <summary>
    /// 获得/设置 Entry 名称 默认 null
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 Entry 实例 不为空
    /// </summary>
    public TEntry? Entry { get; set; }
}
