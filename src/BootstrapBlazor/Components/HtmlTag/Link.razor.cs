// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Link 组件</para>
///  <para lang="en">Link Component</para>
/// </summary>
public partial class Link
{
    /// <summary>
    ///  <para lang="zh">获得/设置 href 属性值</para>
    ///  <para lang="en">Get/Set href Property Value</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? Href { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Rel 属性值, 默认 stylesheet</para>
    ///  <para lang="en">Get/Set Rel Property Value, Default stylesheet</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Rel { get; set; } = "stylesheet";

    /// <summary>
    ///  <para lang="zh">获得/设置 版本号 默认 null 自动生成</para>
    ///  <para lang="en">Get/Set Version Number Default null Auto Generated</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Version { get; set; }

    [Inject, NotNull]
    private IVersionService? VersionService { get; set; }

    private string GetHref() => $"{Href}?v={Version ?? VersionService.GetVersion(Href)}";
}
