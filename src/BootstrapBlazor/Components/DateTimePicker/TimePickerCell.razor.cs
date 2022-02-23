// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 时间选择滚轮单元组件
/// </summary>
public partial class TimePickerCell : IDisposable
{
    private ElementReference TimeCellElement { get; set; }

    /// <summary>
    /// 获得 当前样式名称
    /// </summary>
    private string? GetClassName(int index) => CssBuilder.Default("time-spinner-item")
        .AddClass("prev", ViewModel switch
        {
            TimePickerCellViewModel.Hour => Value.Hours - 1 == index,
            TimePickerCellViewModel.Minute => Value.Minutes - 1 == index,
            _ => Value.Seconds - 1 == index
        })
        .AddClass("active", ViewModel switch
        {
            TimePickerCellViewModel.Hour => Value.Hours == index,
            TimePickerCellViewModel.Minute => Value.Minutes == index,
            _ => Value.Seconds == index
        })
        .AddClass("next", ViewModel switch
        {
            TimePickerCellViewModel.Hour => Value.Hours + 1 == index,
            TimePickerCellViewModel.Minute => Value.Minutes + 1 == index,
            _ => Value.Seconds + 1 == index
        })
        .Build();

    /// <summary>
    /// 获得 滚轮单元数据区间
    /// </summary>
    private IEnumerable<int> Range => ViewModel switch
    {
        TimePickerCellViewModel.Hour => Enumerable.Range(0, 24),
        _ => Enumerable.Range(0, 60)
    };

    /// <summary>
    /// 获得 组件单元数据样式
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass($"transform: translateY({CalcTranslateY()}px);")
        .Build();

    /// <summary>
    /// 获得/设置 时间选择框视图模式
    /// </summary>
    [Parameter]
    public TimePickerCellViewModel ViewModel { get; set; }

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public TimeSpan Value { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化时委托方法
    /// </summary>
    [Parameter]
    public EventCallback<TimeSpan> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 时间刻度行高
    /// </summary>
    [Parameter]
    public Func<double>? ItemHeightCallback { get; set; }

    private JSInterop<TimePickerCell>? Interop { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (Interop == null)
            {
                Interop = new JSInterop<TimePickerCell>(JSRuntime);
            }
            await Interop.InvokeVoidAsync(this, TimeCellElement, "bb_timecell", nameof(OnClickUp), nameof(OnClickDown));
        }
    }

    /// <summary>
    /// 上翻页按钮调用此方法
    /// </summary>
    [JSInvokable]
    public async Task OnClickUp()
    {
        var ts = ViewModel switch
        {
            TimePickerCellViewModel.Hour => TimeSpan.FromHours(1),
            TimePickerCellViewModel.Minute => TimeSpan.FromMinutes(1),
            TimePickerCellViewModel.Second => TimeSpan.FromSeconds(1),
            _ => TimeSpan.Zero
        };
        Value = Value.Subtract(ts);
        if (Value < TimeSpan.Zero)
        {
            Value = Value.Add(TimeSpan.FromHours(24));
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    /// <summary>
    /// 下翻页按钮调用此方法
    /// </summary>
    [JSInvokable]
    public async Task OnClickDown()
    {
        var ts = ViewModel switch
        {
            TimePickerCellViewModel.Hour => TimeSpan.FromHours(1),
            TimePickerCellViewModel.Minute => TimeSpan.FromMinutes(1),
            _ => TimeSpan.FromSeconds(1)
        };
        Value = Value.Add(ts);
        if (Value.Days > 0)
        {
            Value = Value.Subtract(TimeSpan.FromDays(1));
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private double CalcTranslateY()
    {
        var height = ItemHeightCallback?.Invoke() ?? 36.594d;
        return 0 - ViewModel switch
        {
            TimePickerCellViewModel.Hour => (Value.Hours) * height,
            TimePickerCellViewModel.Minute => (Value.Minutes) * height,
            _ => (Value.Seconds) * height
        };
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
