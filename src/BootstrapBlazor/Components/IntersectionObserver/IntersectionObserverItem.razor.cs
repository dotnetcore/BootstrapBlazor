// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">检测交叉组件子组件
///</para>
/// <para lang="en">检测交叉component子component
///</para>
/// </summary>
public partial class IntersectionObserverItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件
    ///</para>
    /// <para lang="en">Gets or sets 子component
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-intersection-observer-item")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
