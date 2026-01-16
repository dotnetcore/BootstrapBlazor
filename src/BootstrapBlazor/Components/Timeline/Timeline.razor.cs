// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">时间线组件基类
///</para>
/// <para lang="en">时间线component基类
///</para>
/// </summary>
public partial class Timeline
{
    /// <summary>
    /// <para lang="zh">获得 Timeline 样式
    ///</para>
    /// <para lang="en">Gets Timeline style
    ///</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("timeline")
        .AddClass("is-alternate", IsAlternate && !IsLeft)
        .AddClass("is-left", IsLeft)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 绑定数据集
    ///</para>
    /// <para lang="en">Gets or sets 绑定data集
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TimelineItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否反转
    ///</para>
    /// <para lang="en">Gets or sets whether反转
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsReverse { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否左右交替出现 默认 false
    ///</para>
    /// <para lang="en">Gets or sets whether左右交替出现 Default is false
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsAlternate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容是否出现在时间线左侧 默认为 false
    ///</para>
    /// <para lang="en">Gets or sets contentwhether出现在时间线左侧 Default is为 false
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsLeft { get; set; }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法
    ///</para>
    /// <para lang="en">OnParametersSet 方法
    ///</para>
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
