// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreen 配置类
/// </summary>
public class FullScreenOption
{
    /// <summary>
    /// 获得/设置 要全屏的 HTML Element 实例
    /// </summary>
    public ElementReference Element { get; set; }

    /// <summary>
    /// 获得/设置 要全屏的 HTML Element Id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 获得/设置 要全屏的 HTML css selector
    /// </summary>
    public string? Selector { get; set; }
}
