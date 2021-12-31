// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
