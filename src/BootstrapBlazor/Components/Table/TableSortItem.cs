// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 排序项 (高级排序使用)
/// </summary>
public class TableSortItem
{
    /// <summary>
    /// 排序字段名
    /// </summary>
    public string SortName { get; set; } = string.Empty;

    /// <summary>
    /// 排序顺序
    /// </summary>
    public SortOrder SortOrder { get; set; }
}

