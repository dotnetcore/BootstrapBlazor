// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart图例显示位置枚举
/// </summary>
public enum ChartLegendPosition
{
    /// <summary>
    /// 上
    /// </summary>
    [Description("top")]
    Top = 1,

    /// <summary>
    /// 下
    /// </summary>
    [Description("bottom")]
    Bottom = 2,

    /// <summary>
    /// 左
    /// </summary>
    [Description("left")]
    Left = 3,

    /// <summary>
    /// 右
    /// </summary>
    [Description("right")]
    Right = 4,
}
