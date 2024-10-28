// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public enum StackAlignItems
{
    /// <summary>
    /// 
    /// </summary>
    [Description("align-items-stretch")]
    Stretch,

    /// <summary>
    /// 
    /// </summary>
    [Description("align-items-start")]
    Start,

    /// <summary>
    /// 
    /// </summary>
    [Description("align-items-center")]
    Center,

    /// <summary>
    /// 
    /// </summary>
    [Description("align-items-end")]
    End,

    /// <summary>
    /// 
    /// </summary>
    [Description("align-items-baseline")]
    Baseline,
}
