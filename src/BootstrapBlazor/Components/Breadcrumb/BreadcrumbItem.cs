// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BreadcrumbItem 类</para>
/// <para lang="en">BreadcrumbItem Class</para>
/// </summary>
/// <param name="text"></param>
/// <param name="url"></param>
/// <param name="cssClass"></param>
public class BreadcrumbItem(string text, string? url = null, string? cssClass = null)
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Gets or sets the display text</para>
    /// </summary>
    public string Text { get; } = text;

    /// <summary>
    /// <para lang="zh">获得/设置 导航地址</para>
    /// <para lang="en">Gets or sets the navigation URL</para>
    /// </summary>
    public string? Url { get; } = url;

    /// <summary>
    /// <para lang="zh">获得/设置 样式名称</para>
    /// <para lang="en">Gets or sets the style name</para>
    /// </summary>
    public string? CssClass { get; } = cssClass;
}
