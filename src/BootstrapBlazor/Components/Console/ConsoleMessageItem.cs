// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 控制台消息实体类
/// </summary>
public class ConsoleMessageItem
{
    /// <summary>
    /// 获得/设置 控制台输出消息
    /// </summary>
    [NotNull]
    public string? Message { get; set; }

    /// <summary>
    /// 获得/设置 控制台消息颜色 默认为 White 白色
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式名称 默认 null
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 获得/设置 是否为 Html 原生字符串 默认 false
    /// </summary>
    public bool IsHtml { get; set; }
}
