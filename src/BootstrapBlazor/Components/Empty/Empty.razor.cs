// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Empty 组件</para>
/// <para lang="en">Empty Component</para>
/// </summary>
public partial class Empty
{
    private string? ClassString => CssBuilder.Default("empty")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 图片路径 默认为 null</para>
    /// <para lang="en">Gets or sets Image Source. Default is null</para>
    /// </summary>
    [Parameter]
    public string? Image { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 空状态描述 默认为 无数据</para>
    /// <para lang="en">Gets or sets Description. Default is No Data</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义模板</para>
    /// <para lang="en">Gets or sets Custom Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets Child Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Empty>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= Localizer[nameof(Text)];
    }
}
