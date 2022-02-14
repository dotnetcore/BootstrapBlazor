// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

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

        if (Items == null)
        {
            Items = Enumerable.Empty<TimelineItem>();
        }

        if (IsReverse)
        {
            var arr = Items.Reverse();
            Items = arr;
        }
    }
}
