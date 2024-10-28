// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 表格骨架屏组件
/// </summary>
public partial class SkeletonTable
{
    /// <summary>
    /// 获得/设置 行数 默认 7 行
    /// </summary>
    [Parameter]
    public int Rows { get; set; } = 7;

    /// <summary>
    /// 获得/设置 行数 默认 3 列
    /// </summary>
    [Parameter]
    public int Columns { get; set; } = 3;

    /// <summary>
    /// 获得/设置 是否显示工具栏
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; } = true;

    private string? TableClassString => CssBuilder.Default("skeleton")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
