// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class Circle
{
    /// <summary>
    /// 获得/设置 当前值
    /// </summary>
    [Parameter]
    public int Value { get; set; }

    /// <summary>
    /// 获得/设置 当前进度值
    /// </summary>
    private string? ValueString => $"{Math.Round(((1 - Value * 1.0 / 100) * CircleLength), 2)}";

    /// <summary>
    /// 获得/设置 Title 字符串
    /// </summary>
    private string ValueTitleString => $"{Value}%";
}
