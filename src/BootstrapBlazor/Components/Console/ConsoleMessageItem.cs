// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">控制台消息实体类</para>
/// <para lang="en">Console message entity class</para>
/// </summary>
public class ConsoleMessageItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 控制台输出消息</para>
    /// <para lang="en">Get/Set console output message</para>
    /// </summary>
    [NotNull]
    public string? Message { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 控制台消息颜色 默认为 White 白色</para>
    /// <para lang="en">Get/Set console message color, default is White</para>
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式名称 默认 null</para>
    /// <para lang="en">Get/Set custom style name, default is null</para>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为 Html 原生字符串 默认 false</para>
    /// <para lang="en">Get/Set whether it is Html raw string, default is false</para>
    /// </summary>
    public bool IsHtml { get; set; }
}
