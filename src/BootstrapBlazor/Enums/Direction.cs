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
    /// 
    /// </summary>
    [Description("dropdown")]
    Dropdown,

    /// <summary>
    /// Dropup
    /// </summary>
    [Description("dropup")]
    Dropup,

    /// <summary>
    /// Dropleft
    /// </summary>
    [Description("dropstart")]
    Dropleft,

    /// <summary>
    /// Dropright
    /// </summary>
    [Description("dropend")]
    Dropright
}
