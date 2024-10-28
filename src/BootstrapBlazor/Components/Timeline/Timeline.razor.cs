// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 时间线组件基类
/// </summary>
public partial class Timeline
{
    /// <summary>
    /// 获得 Timeline 样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("timeline")
        .AddClass("is-alternate", IsAlternate && !IsLeft)
        .AddClass("is-left", IsLeft)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TimelineItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否反转
    /// </summary>
    [Parameter]
    public bool IsReverse { get; set; }

    /// <summary>
    /// 获得/设置 是否左右交替出现 默认 false
    /// </summary>
    [Parameter]
    public bool IsAlternate { get; set; }

    /// <summary>
    /// 获得/设置 内容是否出现在时间线左侧 默认为 false
    /// </summary>
    [Parameter]
    public bool IsLeft { get; set; }

    /// <summary>
    /// OnParametersSet 方法
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
