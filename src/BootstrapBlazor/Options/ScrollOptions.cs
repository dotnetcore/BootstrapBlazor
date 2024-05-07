// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
