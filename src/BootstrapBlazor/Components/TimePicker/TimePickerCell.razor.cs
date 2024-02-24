// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 时间选择滚轮单元组件
/// </summary>
public partial class TimePickerCell
{
    private string? ClassString => CssBuilder.Default("time-spinner-wrapper is-arrow")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 当前样式名称
    /// </summary>
    private string? GetClassName(int index) => CssBuilder.Default("time-spinner-item")
        .AddClass("prev", ViewMode switch
        {
            TimePickerCellViewMode.Hour => Value.Hours - 1 == index,
            TimePickerCellViewMode.Minute => Value.Minutes - 1 == index,
            _ => Value.Seconds - 1 == index
        })
        .AddClass("active", ViewMode switch
        {
            TimePickerCellViewMode.Hour => Value.Hours == index,
            TimePickerCellViewMode.Minute => Value.Minutes == index,
            _ => Value.Seconds == index
        })
        .AddClass("next", ViewMode switch
        {
            TimePickerCellViewMode.Hour => Value.Hours + 1 == index,
            TimePickerCellViewMode.Minute => Value.Minutes + 1 == index,
            _ => Value.Seconds + 1 == index
        })
        .Build();

    /// <summary>
    /// 获得 滚轮单元数据区间
    /// </summary>
    private IEnumerable<int> Range => ViewMode switch
    {
        TimePickerCellViewMode.Hour => Enumerable.Range(0, 24),
        _ => Enumerable.Range(0, 60)
    };

    /// <summary>
    /// 获得 组件单元数据样式
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass($"transform: translateY({CalcTranslateY()}px);")
        .Build();

    private string? UpIconString => CssBuilder.Default("time-spinner-arrow time-up")
        .AddClass(UpIcon, !string.IsNullOrEmpty(UpIcon))
        .Build();

    private string? DownIconString => CssBuilder.Default("time-spinner-arrow time-down")
        .AddClass(DownIcon, !string.IsNullOrEmpty(DownIcon))
        .Build();

    /// <summary>
    /// 获得/设置 时间选择框视图模式
    /// </summary>
    [Parameter]
    public TimePickerCellViewMode ViewMode { get; set; }

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
    /// 获得/设置 向上箭头图标
    /// </summary>
    [Parameter]
    public string? UpIcon { get; set; }

    /// <summary>
    /// 获得/设置 向下箭头图标
    /// </summary>
    [Parameter]
    public string? DownIcon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        UpIcon ??= IconTheme.GetIconByKey(ComponentIcons.TimePickerCellUpIcon);
        DownIcon ??= IconTheme.GetIconByKey(ComponentIcons.TimePickerCellDownIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    /// <summary>
    /// 上翻页按钮调用此方法
    /// </summary>
    [JSInvokable]
    public async Task OnClickUp()
    {
        var ts = ViewMode switch
        {
            TimePickerCellViewMode.Hour => TimeSpan.FromHours(1),
            TimePickerCellViewMode.Minute => TimeSpan.FromMinutes(1),
            _ => TimeSpan.FromSeconds(1),
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
    /// 计算单元格高度回调方法
    /// </summary>
    [JSInvokable]
    public void OnHeightCallback(double height) => _height = height;

    /// <summary>
    /// 下翻页按钮调用此方法
    /// </summary>
    [JSInvokable]
    public async Task OnClickDown()
    {
        var ts = ViewMode switch
        {
            TimePickerCellViewMode.Hour => TimeSpan.FromHours(1),
            TimePickerCellViewMode.Minute => TimeSpan.FromMinutes(1),
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

    private double? _height;

    private double CalcTranslateY()
    {
        var height = _height ?? 36.59375d;
        return 0 - ViewMode switch
        {
            TimePickerCellViewMode.Hour => (Value.Hours) * height,
            TimePickerCellViewMode.Minute => (Value.Minutes) * height,
            _ => (Value.Seconds) * height
        };
    }
}
