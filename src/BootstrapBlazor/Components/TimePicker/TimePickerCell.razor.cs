// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">时间选择滚轮单元组件
///</para>
/// <para lang="en">时间选择滚轮单元component
///</para>
/// </summary>
public partial class TimePickerCell
{
    private string? ClassString => CssBuilder.Default("time-spinner")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 当前样式名称
    ///</para>
    /// <para lang="en">Gets 当前style名称
    ///</para>
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
    /// <para lang="zh">获得 滚轮单元数据区间
    ///</para>
    /// <para lang="en">Gets 滚轮单元data区间
    ///</para>
    /// </summary>
    private IEnumerable<int> Range => ViewMode switch
    {
        TimePickerCellViewMode.Hour => Enumerable.Range(0, 24),
        _ => Enumerable.Range(0, 60)
    };

    /// <summary>
    /// <para lang="zh">获得 组件单元数据样式
    ///</para>
    /// <para lang="en">Gets component单元datastyle
    ///</para>
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass($"transform: translateY({CalcTranslateY().ToString(CultureInfo.InvariantCulture)}px);")
        .Build();

    private string? UpIconString => CssBuilder.Default("time-spinner-arrow time-up")
        .AddClass(UpIcon, !string.IsNullOrEmpty(UpIcon))
        .Build();

    private string? DownIconString => CssBuilder.Default("time-spinner-arrow time-down")
        .AddClass(DownIcon, !string.IsNullOrEmpty(DownIcon))
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 时间选择框视图模式
    ///</para>
    /// <para lang="en">Gets or sets 时间选择框视图模式
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TimePickerCellViewMode ViewMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件值
    ///</para>
    /// <para lang="en">Gets or sets component值
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TimeSpan Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件值变化时委托方法
    ///</para>
    /// <para lang="en">Gets or sets component值变化时delegate方法
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<TimeSpan> ValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 向上箭头图标
    ///</para>
    /// <para lang="en">Gets or sets 向上箭头icon
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? UpIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 向下箭头图标
    ///</para>
    /// <para lang="en">Gets or sets 向下箭头icon
    ///</para>
    /// <para><version>10.2.2</version></para>
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
    /// <para lang="zh">上翻页按钮调用此方法
    ///</para>
    /// <para lang="en">上翻页button调用此方法
    ///</para>
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
    /// <para lang="zh">计算单元格高度回调方法
    ///</para>
    /// <para lang="en">计算单元格heightcallback method
    ///</para>
    /// </summary>
    [JSInvokable]
    public void OnHeightCallback(double height) => _height = height;

    /// <summary>
    /// <para lang="zh">下翻页按钮调用此方法
    ///</para>
    /// <para lang="en">下翻页button调用此方法
    ///</para>
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
