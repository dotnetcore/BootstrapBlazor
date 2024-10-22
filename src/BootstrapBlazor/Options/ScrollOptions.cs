// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ScrollOptions 配置类
/// </summary>
public class ScrollOptions
{
    /// <summary>
    /// 获得/设置 滚动条宽度 默认 5px
    /// </summary>
    public int ScrollWidth { get; set; } = 5;

    /// <summary>
    /// 获得/设置 滚动条鼠标悬浮宽度 默认 5px
    /// </summary>
    public int ScrollHoverWidth { get; set; } = 5;
}
