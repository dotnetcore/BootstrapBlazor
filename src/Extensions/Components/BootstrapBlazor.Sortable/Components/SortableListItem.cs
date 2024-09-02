// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SortableListItem 类
/// </summary>
public class SortableListItem
{
    /// <summary>
    /// 获得/设置 原始索引
    /// </summary>
    public int OldIndex { get; set; }

    /// <summary>
    /// 获得/设置 新索引
    /// </summary>
    public int NewIndex { get; set; }
}
