// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// RangeSlider
/// </summary>
public partial class RangeSlider
{
    /// <summary>
    /// 获得/设置 组件当前值
    /// </summary>
    [Parameter]
    public double Value { get; set; }

    /// <summary>
    /// ValueChanged 回调方法
    /// </summary>
    [Parameter]
    public EventCallback<double> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 值变化时回调方法
    /// </summary>
    [Parameter]
    public Func<double, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 最大值
    /// </summary>
    [Parameter]
    public double Max { get; set; } = 100;

    /// <summary>
    /// 获得/设置 最小值
    /// </summary>
    [Parameter]
    public double Min { get; set; } = 0;

    /// <summary>
    /// 获得/设置 显示Value值
    /// </summary>
    [Parameter]
    public bool ShowValueLabel { get; set; } = true;

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("range-slider")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task oninputAsync(ChangeEventArgs args)
    {
        Value = Convert.ToDouble(args.Value);
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
