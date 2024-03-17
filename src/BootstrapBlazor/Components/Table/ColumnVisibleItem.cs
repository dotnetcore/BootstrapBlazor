// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件列可见性类
/// </summary>
/// <param name="name"></param>
/// <param name="visible"></param>
public class ColumnVisibleItem(string name, bool visible)
{
    /// <summary>
    /// 获得 列名称
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// 获得 列名称
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// 获得 列可见性
    /// </summary>
    public bool Visible { get; set; } = visible;
}
