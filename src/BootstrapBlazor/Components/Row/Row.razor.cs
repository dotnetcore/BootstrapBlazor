// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Row
{
    /// <summary>
    /// 获得/设置 设置一行显示多少个子组件
    /// </summary>
    [Parameter]
    public ItemsPerRow ItemsPerRow { get; set; }

    /// <summary>
    /// 获得/设置 设置行格式 默认 Row 布局
    /// </summary>
    [Parameter]
    public RowType RowType { get; set; }

    /// <summary>
    /// 获得/设置 子 Row 跨父 Row 列数 默认为 null
    /// </summary>
    [Parameter]
    public int? ColSpan { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
