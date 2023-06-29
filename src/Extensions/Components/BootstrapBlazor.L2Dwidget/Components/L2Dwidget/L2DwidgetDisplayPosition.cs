// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// L2Dwidget 显示模式枚举
/// </summary>
public enum L2DwidgetDisplayPosition
{
    /// <summary>
    /// LeftBottom 左下角
    /// </summary>
    [Description("left")]
    LeftBottom,

    /// <summary>
    /// RightBottom 右下角
    /// </summary>
    [Description("right")]
    RightBottom
}
