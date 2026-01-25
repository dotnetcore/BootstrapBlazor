// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapInputGroupIcon 组件</para>
/// <para lang="en">BootstrapInputGroupIcon Component</para>
/// </summary>
public partial class BootstrapInputGroupIcon
{
    private string? ClassString => CssBuilder.Default("input-group-text")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 Icon</para>
    /// <para lang="en">Gets or sets Icon</para>
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? Icon { get; set; }
}
