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

    /// <summary>
    /// 获得/设置 最小值 默认为 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Min { get; set; }

    /// <summary>
    /// 获得/设置 最大值 默认为 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Max { get; set; }

    /// <summary>
    /// 获得/设置 步长 默认为 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Step { get; set; }

    private string eventName => UseInputEvent ? "oninput" : "onchange";

    private string? MinString => Min.ToString() == "0" ? null : Min.ToString();

    private string? MaxString => Max.ToString() == "0" ? null : Max.ToString();

    private string? StepString => Step.ToString() == "0" ? null : Step.ToString();
}
