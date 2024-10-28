// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public enum StackJustifyContent
{
    /// <summary>
    /// 
    /// </summary>
    [Description("justify-content-start")]
    Start,

    /// <summary>
    /// 
    /// </summary>
    [Description("justify-content-center")]
    Center,

    /// <summary>
    /// 
    /// </summary>
    [Description("justify-content-end")]
    End,

    /// <summary>
    /// 
    /// </summary>
    [Description("justify-content-between")]
    Between,

    /// <summary>
    /// 
    /// </summary>
    [Description("justify-content-around")]
    Around,

    /// <summary>
    /// 
    /// </summary>
    [Description("justify-content-evenly")]
    Evenly,
}
