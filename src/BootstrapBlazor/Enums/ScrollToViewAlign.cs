// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 垂直滚动对齐方式
/// </summary>
public enum ScrollToViewAlign
{
    /// <summary>
    /// 
    /// </summary>
    [Description("start")]
    Start,

    /// <summary>
    /// 
    /// </summary>
    [Description("center")]
    Center,

    /// <summary>
    /// 
    /// </summary>
    [Description("end")]
    End,

    /// <summary>
    /// 
    /// </summary>
    [Description("nearest")]
    Nearest
}
