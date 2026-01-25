// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">时间线组件基类</para>
/// <para lang="en">Timeline Component Base Class</para>
/// </summary>
public partial class Timeline
{
    /// <summary>
    /// <para lang="zh">获得 Timeline 样式</para>
    /// <para lang="en">Gets the Timeline style</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("timeline")
        .AddClass("is-alternate", IsAlternate && !IsLeft)
        .AddClass("is-left", IsLeft)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 绑定数据集合</para>
    /// <para lang="en">Gets or sets the bound data collection</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TimelineItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否反转</para>
    /// <para lang="en">Gets or sets whether to reverse</para>
    /// </summary>
    [Parameter]
    public bool IsReverse { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否左右交替出现，默认 false</para>
    /// <para lang="en">Gets or sets whether items alternate left and right. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsAlternate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容是否出现在时间线左侧，默认为 false</para>
    /// <para lang="en">Gets or sets whether content appears on the left side of the timeline. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsLeft { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<TimelineItem>();

        if (IsReverse)
        {
            var arr = Items.Reverse();
            Items = arr;
        }
    }
}
