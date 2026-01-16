// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class Circle
{
    /// <summary>
    /// 获得/设置 当前值
    /// <para><version>10.0.0</version></para>
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
