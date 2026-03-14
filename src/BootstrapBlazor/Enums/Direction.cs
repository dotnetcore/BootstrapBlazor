// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">下拉框枚举类</para>
/// <para lang="en">Dropdown Direction Enum</para>
/// </summary>
public enum Direction
{
    /// <summary>
    /// <para lang="zh">向下</para>
    /// <para lang="en">Down</para>
    /// </summary>
    [Description("dropdown")]
    Dropdown,

    /// <summary>
    /// <para lang="zh">向上</para>
    /// <para lang="en">Up</para>
    /// </summary>
    [Description("dropup")]
    Dropup,

    /// <summary>
    /// <para lang="zh">向左</para>
    /// <para lang="en">Left</para>
    /// </summary>
    [Description("dropstart")]
    Dropleft,

    /// <summary>
    /// <para lang="zh">向右</para>
    /// <para lang="en">Right</para>
    /// </summary>
    [Description("dropend")]
    Dropright
}
