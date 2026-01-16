// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Badge 徽章组件</para>
/// <para lang="en">Badge component</para>
/// </summary>
public partial class Badge
{
    private string? ClassString => CssBuilder.Default("badge")
        .AddClass($"text-bg-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("rounded-pill", IsPill)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 徽章颜色 默认为 <see cref="Color.Primary"/></para>
    /// <para lang="en">Gets or sets the color of the badge. Default is <see cref="Color.Primary"/>.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 徽章是否显示为胶囊形式 默认为 false</para>
    /// <para lang="en">Gets or sets whether the badge should be displayed as a pill (rounded) or not. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsPill { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件内容 默认为 false</para>
    /// <para lang="en">Gets or sets the child content of the component. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
