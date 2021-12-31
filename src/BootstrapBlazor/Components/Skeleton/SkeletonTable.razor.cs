// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
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
}
