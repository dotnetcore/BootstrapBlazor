// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FullScreen 配置类</para>
/// <para lang="en">FullScreen Configuration Class</para>
/// </summary>
public class FullScreenOption
{
    /// <summary>
    /// <para lang="zh">获得/设置 要全屏的 HTML Element 实例</para>
    /// <para lang="en">Gets or sets HTML Element Instance to be Full Screen</para>
    /// </summary>
    public ElementReference Element { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 要全屏的 HTML Element Id</para>
    /// <para lang="en">Gets or sets HTML Element Id to be Full Screen</para>
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 要全屏的 HTML css selector</para>
    /// <para lang="en">Gets or sets HTML css selector to be Full Screen</para>
    /// </summary>
    public string? Selector { get; set; }
}
