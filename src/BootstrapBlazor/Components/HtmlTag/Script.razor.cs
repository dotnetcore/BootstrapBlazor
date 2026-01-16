// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Script 组件</para>
/// <para lang="en">Script Component</para>
/// </summary>
public partial class Script
{
    /// <summary>
    /// <para lang="zh">获得/设置 src 属性值</para>
    /// <para lang="en">Get/Set src Property Value</para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? Src { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 版本号 默认 null 自动生成</para>
    /// <para lang="en">Get/Set Version Number Default null Auto Generated</para>
    /// </summary>
    [Parameter]
    public string? Version { get; set; }

    [Inject, NotNull]
    private IVersionService? VersionService { get; set; }

    private string GetSrc() => $"{Src}?v={Version ?? VersionService.GetVersion(Src)}";
}
