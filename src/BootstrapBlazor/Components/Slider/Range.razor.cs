// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Range 组件
/// </summary>
public partial class Range<TValue>
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassString => CssBuilder.Default("form-range")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 是否使用 input 事件 默认为 false
    /// </summary>
    [Parameter]
    public bool UseInputEvent { get; set; }

    private string eventName => UseInputEvent ? "oninput" : "onchange";
}
