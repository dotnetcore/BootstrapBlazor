// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart DataLabel 对齐方式
/// </summary>
public enum ChartDataLabelPosition
{
    /// <summary>
    /// 上
    /// </summary>
    [Description("start")]
    Start = 1,

    /// <summary>
    /// 下
    /// </summary>
    [Description("center")]
    Center = 2,

    /// <summary>
    /// 左
    /// </summary>
    [Description("end")]
    End = 3,
}
