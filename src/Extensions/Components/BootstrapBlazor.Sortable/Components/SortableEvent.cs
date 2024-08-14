// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SortableEvent 类
/// </summary>
public class SortableEvent
{
    /// <summary>
    /// 获得/设置 原始索引
    /// </summary>
    [NotNull]
    public int OldIndex { get; set; }

    /// <summary>
    /// 获得/设置 新索引
    /// </summary>
    [NotNull]
    public int NewIndex { get; set; }

    /// <summary>
    /// 获得/设置 移动元素 <see cref="SortableListItem"/> 集合
    /// </summary>
    public List<SortableListItem> Items { get; } = [];
}
