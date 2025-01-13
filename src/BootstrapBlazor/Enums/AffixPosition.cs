// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// AffixPosition 枚举
/// </summary>
public enum AffixPosition
{
    /// <summary>
    /// 固定在顶部
    /// </summary>
    [Description("top")]
    Top,

    /// <summary>
    /// 固定在底部
    /// </summary>
    [Description("bottom")]
    Bottom
}
