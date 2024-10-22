// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
