// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">AffixPosition 枚举</para>
/// <para lang="en">AffixPosition Enum</para>
/// </summary>
public enum AffixPosition
{
    /// <summary>
    /// <para lang="zh">固定在顶部</para>
    /// <para lang="en">Fixed at the top</para>
    /// </summary>
    [Description("top")]
    Top,

    /// <summary>
    /// <para lang="zh">固定在底部</para>
    /// <para lang="en">Fixed at the bottom</para>
    /// </summary>
    [Description("bottom")]
    Bottom
}
