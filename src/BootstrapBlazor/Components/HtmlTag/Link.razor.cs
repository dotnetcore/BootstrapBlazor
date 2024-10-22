﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Link 组件
/// </summary>
public partial class Link
{
    /// <summary>
    /// 获得/设置 href 属性值
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? Href { get; set; }

    /// <summary>
    /// 获得/设置 Rel 属性值, 默认 stylesheet
    /// </summary>
    [Parameter]
    public string? Rel { get; set; } = "stylesheet";

    /// <summary>
    /// 获得/设置 版本号 默认 null 自动生成
    /// </summary>
    [Parameter]
    public string? Version { get; set; }

    [Inject, NotNull]
    private IVersionService? VersionService { get; set; }

    private string GetHref() => $"{Href}?v={Version ?? VersionService.GetVersion(Href)}";
}
