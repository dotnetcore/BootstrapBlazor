// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表格骨架屏组件</para>
/// <para lang="en">SkeletonTable Component</para>
/// </summary>
public partial class SkeletonTable
{
    /// <summary>
    /// <para lang="zh">获得/设置 行数 默认 7 行</para>
    /// <para lang="en">Get/Set Rows. Default 7</para>
    /// </summary>
    [Parameter]
    public int Rows { get; set; } = 7;

    /// <summary>
    /// <para lang="zh">获得/设置 行数 默认 3 列</para>
    /// <para lang="en">Get/Set Columns. Default 3</para>
    /// </summary>
    [Parameter]
    public int Columns { get; set; } = 3;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示工具栏</para>
    /// <para lang="en">Get/Set Whether to show toolbar</para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; } = true;

    private string? TableClassString => CssBuilder.Default("skeleton")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
