// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimePicker 组件基类
/// </summary>
public sealed partial class DateTimePicker<TValue>
{
    /// <summary>
    /// 获得 组件样式名称
    /// </summary>
    private string? ClassString => CssBuilder.Default("select datetime-picker")
        .AddClass("disabled", IsDisabled)
        .AddClass(ValidCss)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 组件文本框样式名称
    /// </summary>
    private string? InputClassName => CssBuilder.Default("dropdown-toggle form-control datetime-picker-input")
        .AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 组件小图标样式
    /// </summary>
    private string? DateTimePickerIconClassString => CssBuilder.Default("datetime-picker-bar")
        .AddClass(Icon)
        .Build();

    /// <summary>
    /// 获得 组件弹窗位置
    /// </summary>
    private string? PlacementString => CssBuilder.Default()
        .AddClass(Placement.ToDescriptionString(), Placement != Placement.Auto)
        .Build();

    /// <summary>
    /// 获得 Placeholder 显示字符串
    /// </summary>
    private string? PlaceholderString => ViewMode switch
    {
        DatePickerViewMode.DateTime => DateTimePlaceHolderText,
        _ => DatePlaceHolderText
    };

    /// <summary>
    /// 获得/设置 是否允许为空 默认 false 不允许为空
    /// </summary>
    private bool AllowNull { get; set; }

    /// <summary>
    /// 获得/设置 组件时间
    /// </summary>
    private DateTime ComponentValue
    {
        get
        {
            var v = DateTime.Now;
            if (AllowNull)
            {
                var t = Value as DateTime?;
                if (t.HasValue) v = t.Value;
            }
            else
            {
                var t = (DateTime)(object)Value;
                v = t;
            }
            return ViewMode == DatePickerViewMode.Date ? v.Date : v;
        }
        set
        {
            CurrentValue = (TValue)(object)value;
        }
    }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd"
    /// </summary>
    [Parameter]
    public string? Format { get; set; }

    /// <summary>
    /// 获得/设置 组件图标 默认 fa-regular fa-calendar-days
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 弹窗位置 默认为 Auto
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Auto;

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    [Parameter]
    public DatePickerViewMode ViewMode { get; set; } = DatePickerViewMode.Date;

    /// <summary>
    /// 获得/设置 是否显示快捷侧边栏 默认不显示
    /// </summary>
    [Parameter]
    public bool ShowSidebar { get; set; }

    /// <summary>
    /// 获得/设置 当前日期最大值
    /// </summary>
    [Parameter]
    public DateTime? MaxValue { get; set; }

    /// <summary>
    /// 获得/设置 当前日期最小值
    /// </summary>
    [Parameter]
    public DateTime? MinValue { get; set; }

    /// <summary>
    /// 获得/设置 当前日期变化时回调委托方法
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnDateTimeChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否点击日期后自动关闭弹窗 默认 true
    /// </summary>
    [Parameter]
    public bool AutoClose { get; set; } = true;

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

    [NotNull]
    private string? DatePlaceHolderText { get; set; }

    [NotNull]
    private string? DateTimePlaceHolderText { get; set; }

    [NotNull]
    private string? GenericTypeErroMessage { get; set; }

    [NotNull]
    private string? DateTimeFormat { get; set; }

    [NotNull]
    private string? DateFormat { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 判断泛型类型
        var isDateTime = typeof(TValue) == typeof(DateTime) || typeof(TValue) == typeof(DateTime?);
        if (!isDateTime) throw new InvalidOperationException(GenericTypeErroMessage);

        // 泛型设置为可为空
        AllowNull = typeof(TValue) == typeof(DateTime?);
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DateTimePlaceHolderText ??= Localizer[nameof(DateTimePlaceHolderText)];
        DatePlaceHolderText ??= Localizer[nameof(DatePlaceHolderText)];
        GenericTypeErroMessage ??= Localizer[nameof(GenericTypeErroMessage)];
        DateTimeFormat ??= Localizer[nameof(DateTimeFormat)];
        DateFormat ??= Localizer[nameof(DateFormat)];

        Icon ??= "fa-regular fa-calendar-days";

        // Value 为 MinValue 时 设置 Value 默认值
        if (Value?.ToString() == DateTime.MinValue.ToString())
        {
            CurrentValue = (TValue)(object)DateTime.Now;
        }
    }

    /// <summary>
    /// 格式化数值方法
    /// </summary>
    protected override string FormatValueAsString(TValue value)
    {
        var ret = "";
        if (value != null)
        {
            var format = Format;
            if (string.IsNullOrEmpty(format))
            {
                format = ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat;
            }

            ret = ComponentValue.ToString(format);
        }
        return ret;
    }

    /// <summary>
    /// 清空按钮点击时回调此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClear()
    {
        CurrentValue = default;
        if (OnDateTimeChanged != null)
        {
            await OnDateTimeChanged(Value);
        }
        StateHasChanged();
    }

    /// <summary>
    /// 确认按钮点击时回调此方法
    /// </summary>
    private async Task OnConfirm()
    {
        if (AutoClose)
        {
            await JSRuntime.InvokeVoidAsync(identifier: "bb.Dropdown.invoke", $"#{Id}", "hide");
        }

        if (OnDateTimeChanged != null)
        {
            await OnDateTimeChanged(Value);
        }
    }
}
