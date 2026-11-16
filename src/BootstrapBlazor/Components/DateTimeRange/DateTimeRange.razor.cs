// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DateTimeRange 时间范围组件</para>
/// <para lang="en">DateTimeRange component</para>
/// </summary>
public partial class DateTimeRange
{
    /// <summary>
    /// <para lang="zh">获得 组件样式名称</para>
    /// <para lang="en">Get Component Class Name</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("select datetime-range form-control")
        .AddClass("disabled", IsDisabled)
        .AddClass("has-time", ViewMode == DatePickerViewMode.DateTime)
        .AddClass(ValidCss)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 组件小图标样式</para>
    /// <para lang="en">Get Component Icon Class Name</para>
    /// </summary>
    private string? DateTimePickerIconClassString => CssBuilder.Default("range-bar")
        .AddClass(Icon)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 用户选中的时间范围</para>
    /// <para lang="en">Get User Selected Date Range</para>
    /// </summary>
    internal DateTimeRangeValue SelectedValue { get; } = new DateTimeRangeValue();

    private static readonly TimeSpan _startTimeValue = TimeSpan.Zero;
    private static readonly TimeSpan _endTimeValue = new(23, 59, 59);

    private DateTime StartValue { get; set; }

    private string? StartValueString
    {
        set
        {
            var format = ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat;
            var culture = ViewMode == DatePickerViewMode.DateTime ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture;
            var ret = DateTime.TryParseExact(value, format, culture, DateTimeStyles.None, out var startDateValue);
            if (ret)
            {
                StartValue = startDateValue;
                Value.Start = startDateValue;
                SelectedValue.Start = startDateValue;
            }
        }
        get
        {
            var format = ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat;
            return Value.Start != DateTime.MinValue ? Value.Start.ToString(format) : null;
        }
    }

    private DateTime EndValue { get; set; }

    private string? EndValueString
    {
        set
        {
            var format = ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat;
            var culture = ViewMode == DatePickerViewMode.DateTime ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture;
            var ret = DateTime.TryParseExact(value, format, culture, DateTimeStyles.None, out var endDateValue);
            if (ret)
            {
                EndValue = endDateValue;
                Value.End = endDateValue;
                SelectedValue.End = endDateValue;
            }
        }
        get
        {
            var format = ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat;
            return Value.End != DateTime.MinValue ? Value.End.ToString(format) : null;
        }
    }

    /// <summary>
    /// <para lang="zh">获得/设置 开始日期占位文本，默认使用本地化文本</para>
    /// <para lang="en">Gets or sets the start date placeholder text. Defaults to localized text.</para>
    /// <para><version>11.1.1</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? StartPlaceHolderText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 结束日期占位文本，默认使用本地化文本</para>
    /// <para lang="en">Gets or sets the end date placeholder text. Defaults to localized text.</para>
    /// <para><version>11.1.1</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? EndPlaceHolderText { get; set; }

    [NotNull]
    private string? SeparateText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可以编辑内容 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Allow Edit. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否点击快捷侧边栏自动关闭弹窗 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Auto Close Popup When Sidebar Item Clicked. Default is false</para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool AutoCloseClickSideBar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择日期范围后是否自动关闭弹窗，默认为 false。日期时间模式下选择日期后需要手动确认。</para>
    /// <para lang="en">Gets or sets whether to automatically close the popup after a date range is selected. Default is false. DateTime mode requires manual confirmation after selecting dates.</para>
    /// </summary>
    [Parameter]
    public bool AutoClose { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 show the selected value. 默认为 false</para>
    /// <para lang="en">Gets or sets whether show the selected value. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowSelectedValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空按钮文字</para>
    /// <para lang="en">Gets or sets Clear Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空图标 默认 fa-solid fa-circle-xmark</para>
    /// <para lang="en">Gets or sets Clear Icon. Default is fa-solid fa-circle-xmark</para>
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件显示模式 默认为显示年月日模式</para>
    /// <para lang="en">Gets or sets Component Display Mode. Default is Date Mode</para>
    /// </summary>
    [Parameter]
    public DatePickerViewMode ViewMode { get; set; } = DatePickerViewMode.Date;

    /// <summary>
    /// <para lang="zh">获得/设置 日历视图数量，默认为双视图；屏幕宽度小于 768px 时始终显示单视图</para>
    /// <para lang="en">Gets or sets the calendar layout. Defaults to double view; below 768px, always displays a single view</para>
    /// </summary>
    [Parameter]
    public DateTimeRangeRenderMode RenderMode { get; set; } = DateTimeRangeRenderMode.Double;

    /// <summary>
    /// <para lang="zh">获得/设置 今天按钮文字</para>
    /// <para lang="en">Gets or sets Today Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TodayButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确定按钮文字</para>
    /// <para lang="en">Gets or sets Confirm Button Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最大值</para>
    /// <para lang="en">Gets or sets Max Value</para>
    /// </summary>
    [Parameter]
    public DateTime MaxValue { get; set; } = DateTime.MaxValue;
    /// <summary>
    /// <para lang="zh">获得/设置 最小值</para>
    /// <para lang="en">Gets or sets Min Value</para>
    /// </summary>
    [Parameter]
    public DateTime MinValue { get; set; } = DateTime.MinValue;

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许为空 默认为 true</para>
    /// <para lang="en">Gets or sets Whether to Allow Null. Default is true</para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ShowClearButton 代替；Deprecated, please use ShowClearButton instead")]
    [ExcludeFromCodeCoverage]
    public bool AllowNull
    {
        get => ShowClearButton;
        set => ShowClearButton = value;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示清空按钮 默认 true</para>
    /// <para lang="en">Gets or sets Whether to Show Clear Button. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 组件图标</para>
    /// <para lang="en">Gets or sets Component Icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示今天按钮 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to Show Today Button. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowToday { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示快捷侧边栏 默认不显示</para>
    /// <para lang="en">Gets or sets Whether to Show Sidebar. Default is not shown</para>
    /// </summary>
    [Parameter]
    public bool ShowSidebar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏快捷选项集合</para>
    /// <para lang="en">Gets or sets Sidebar Items</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public List<DateTimeRangeSidebarItem>? SidebarItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 侧边栏模板 默认 null，模板上下文为选择日期范围的回调</para>
    /// <para lang="en">Gets or sets Sidebar Template. Default is null; the template context is a callback for selecting a date range</para>
    /// <para><version>11.1.0</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<Func<DateTimeRangeValue, Task>>? SidebarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">点击确认按钮回调委托方法</para>
    /// <para lang="en">Callback Method When Confirm Button Clicked</para>
    /// </summary>
    [Parameter]
    public Func<DateTimeRangeValue, Task>? OnConfirm { get; set; }

    /// <summary>
    /// <para lang="zh">点击清空按钮回调委托方法</para>
    /// <para lang="en">Callback Method When Clear Button Clicked</para>
    /// </summary>
    [Parameter]
    public Func<DateTimeRangeValue, Task>? OnClearValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间格式化字符串 默认值为 "hh\:mm\:ss"</para>
    /// <para lang="en">Gets or sets Time Format String. Default is "hh\:mm\:ss"</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd"</para>
    /// <para lang="en">Gets or sets Date Format String. Default is "yyyy-MM-dd"</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateFormat { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd HH:mm:ss"</para>
    /// <para lang="en">Gets or sets Date Time Format String. Default is "yyyy-MM-dd HH:mm:ss"</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateTimeFormat { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示中国阴历历法 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Chinese Lunar Calendar. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowLunar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示中国 24 节气 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Chinese Solar Term. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowSolarTerm { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示节日 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Festivals. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowFestivals { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示休假日 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Holidays. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowHolidays { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the date value changed event 回调</para>
    /// <para lang="en">Gets or sets the date value changed event callback</para>
    /// </summary>
    [Parameter]
    public Func<DateTime, Task>? OnDateClick { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimeRange>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ReadonlyString => IsEditable ? null : "readonly";

    private string? ValueClassString => CssBuilder.Default("datetime-range-input")
        .AddClass("datetime", ViewMode == DatePickerViewMode.DateTime)
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? DialogHeaderText => DisplayText ?? $"{StartPlaceHolderText} {SeparateText} {EndPlaceHolderText}";

    private string? EndPanelClassString => CssBuilder.Default("range-end-panel")
        .AddClass("is-double", RenderMode == DateTimeRangeRenderMode.Double)
        .Build();

    private bool IsConfirmDisabled => ViewMode == DatePickerViewMode.DateTime
        && ((SelectedValue.Start != DateTime.MinValue && (SelectedValue.Start < MinValue || SelectedValue.Start > MaxValue))
            || (SelectedValue.End != DateTime.MinValue && (SelectedValue.End < MinValue || SelectedValue.End > MaxValue))
            || (SelectedValue.Start != DateTime.MinValue && SelectedValue.End != DateTime.MinValue && SelectedValue.Start > SelectedValue.End));

    private DatePickerViewMode PickerBodyViewMode => ViewMode == DatePickerViewMode.DateTime ? DatePickerViewMode.Date : ViewMode;

    private DateTime PickerBodyMinValue => ViewMode == DatePickerViewMode.DateTime ? MinValue.Date : MinValue;

    private DateTime PickerBodyMaxValue => ViewMode == DatePickerViewMode.DateTime ? MaxValue.Date : MaxValue;

    private string? TimePanelClassString => CssBuilder.Default("range-panel-time")
        .AddClass("is-single", RenderMode == DateTimeRangeRenderMode.Single)
        .Build();

    private string? GetTimePickerClassString(bool isStart) => CssBuilder.Default("picker-panel-time")
        .AddClass(isStart ? "range-start-time" : "range-end-time")
        .AddClass("show", _activeTimePicker == isStart)
        .Build();

    /// <summary>
    /// 标间 TimerPick 是否显示
    /// null 表示都关闭 true 表示显示开始时间 false 表示显示结束时间
    /// </summary>
    private bool? _activeTimePicker;

    private bool HasSeconds => TimeFormat.Contains('s');

    private TimeSpan GetTime(bool isStart) => isStart ? StartValue.TimeOfDay : EndValue.TimeOfDay;

    private string? GetTimeText(bool isStart) => isStart ? StartPlaceHolderText : EndPlaceHolderText;

    private void ShowTimePicker(bool isStart) => _activeTimePicker = isStart;

    private Task CloseTimePickerAsync()
    {
        _activeTimePicker = null;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task UpdateStartTimeAsync(TimeSpan time)
    {
        StartValue = StartValue.Date + time;
        if (SelectedValue.Start != DateTime.MinValue)
        {
            SelectedValue.Start = SelectedValue.Start.Date + time;
        }

        return UpdateTimeValueAsync();
    }

    private Task UpdateEndTimeAsync(TimeSpan time)
    {
        EndValue = EndValue.Date + time;
        if (SelectedValue.End != DateTime.MinValue)
        {
            SelectedValue.End = SelectedValue.End.Date + time;
        }

        return UpdateTimeValueAsync();
    }

    private Task UpdateTimeValueAsync()
    {
        _activeTimePicker = null;
        if (ShowSelectedValue)
        {
            Value.Start = SelectedValue.Start;
            Value.End = SelectedValue.End;
        }

        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AddRequiredValidator();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        StartPlaceHolderText ??= Localizer[nameof(StartPlaceHolderText)];
        EndPlaceHolderText ??= Localizer[nameof(EndPlaceHolderText)];
        SeparateText ??= Localizer[nameof(SeparateText)];

        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        TodayButtonText ??= Localizer[nameof(TodayButtonText)];

        DateFormat ??= Localizer[nameof(DateFormat)];
        DateTimeFormat ??= Localizer[nameof(DateTimeFormat)];
        TimeFormat ??= Localizer[nameof(TimeFormat)];

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.DateTimeRangeIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DateTimeRangeClearIcon); ;

        SidebarItems ??= [];

        ResetBodyValue();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new
    {
        TriggerHideCallback = nameof(TriggerHideCallback)
    });

    private bool CanTriggerOnclickSidebarItem(DateTimeRangeSidebarItem item) => !IsDisabled
        && item.StartDateTime <= item.EndDateTime
        && item.StartDateTime >= MinValue && item.EndDateTime <= MaxValue;

    private async Task OnClickSidebarTemplate(DateTimeRangeValue value)
    {
        var item = new DateTimeRangeSidebarItem
        {
            StartDateTime = value.Start,
            EndDateTime = value.End
        };
        if (CanTriggerOnclickSidebarItem(item))
        {
            await OnClickSidebarItem(item);
            StateHasChanged();
        }
    }

    private async Task OnClickSidebarItem(DateTimeRangeSidebarItem item)
    {
        SelectedValue.Start = item.StartDateTime;
        SelectedValue.End = item.EndDateTime;

        StartValue = item.StartDateTime;
        EndValue = StartValue.GetSafeMonthDateTime(1).Date + SelectedValue.End.TimeOfDay;
        await ClickConfirmButton();
    }

    /// <summary>
    /// <para lang="zh">点击 清除按钮调用此方法</para>
    /// <para lang="en">Method Called When Clear Button Clicked</para>
    /// </summary>
    private async Task ClickClearButton()
    {
        Value = new DateTimeRangeValue();
        SelectedValue.NullStart = null;
        SelectedValue.NullEnd = null;

        if (OnClearValue != null)
        {
            await OnClearValue(Value);
        }
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (IsNeedValidate && EditContext != null && FieldIdentifier != null)
        {
            EditContext.NotifyFieldChanged(FieldIdentifier.Value);
        }
        await InvokeVoidAsync("hide", Id);
    }

    private Task OnStartDateChanged(DateTime value)
    {
        StartValue = value.Date + StartValue.TimeOfDay;
        EndValue = ViewMode == DatePickerViewMode.DateTime
            ? StartValue.GetSafeMonthDateTime(1).Date + EndValue.TimeOfDay
            : GetEndDateTime(StartValue.AddMonths(1).Date);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnEndDateChanged(DateTime value)
    {
        EndValue = ViewMode == DatePickerViewMode.DateTime ? value.Date + EndValue.TimeOfDay : GetEndDateTime(value);
        StartValue = value.GetSafeMonthDateTime(-1).Date + StartValue.TimeOfDay;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">点击 确认时调用此方法</para>
    /// <para lang="en">Method Called When Confirm Button Clicked</para>
    /// </summary>
    private async Task ClickTodayButton()
    {
        SelectedValue.Start = DateTime.Today;
        SelectedValue.End = GetEndDateTime(DateTime.Today);

        EndValue = SelectedValue.End;
        StartValue = SelectedValue.Start.GetSafeMonthDateTime(-1);
        await ClickConfirmButton();
    }

    /// <summary>
    /// <para lang="zh">点击 确认时调用此方法</para>
    /// <para lang="en">Method Called When Confirm Button Clicked</para>
    /// </summary>
    private async Task ClickConfirmButton()
    {
        if (ViewMode == DatePickerViewMode.DateTime)
        {
            if (SelectedValue.Start == DateTime.MinValue)
            {
                SelectedValue.Start = DateTime.Today + StartValue.TimeOfDay;
            }
            if (SelectedValue.End == DateTime.MinValue)
            {
                if (SelectedValue.Start.Date > DateTime.Today)
                {
                    SelectedValue.End = SelectedValue.Start.Date + EndValue.TimeOfDay;
                    SelectedValue.Start = DateTime.Today + StartValue.TimeOfDay;
                }
                else
                {
                    SelectedValue.End = DateTime.Today + EndValue.TimeOfDay;
                }
            }
            if (IsConfirmDisabled)
            {
                return;
            }
        }
        else if (SelectedValue.End == DateTime.MinValue)
        {
            if (SelectedValue.Start < DateTime.Today)
            {
                SelectedValue.End = DateTime.Today;
            }
            else
            {
                SelectedValue.End = SelectedValue.Start;
                SelectedValue.Start = DateTime.Today;
            }
        }
        Value.Start = ViewMode == DatePickerViewMode.DateTime ? SelectedValue.Start : SelectedValue.Start.Date;
        Value.End = ViewMode == DatePickerViewMode.DateTime ? SelectedValue.End : GetEndDateTime(SelectedValue.End);

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnConfirm != null)
        {
            await OnConfirm(Value);
        }
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
        if (IsNeedValidate && EditContext != null && FieldIdentifier != null)
        {
            EditContext.NotifyFieldChanged(FieldIdentifier.Value);
        }
        await InvokeVoidAsync("hide", Id);
    }

    /// <summary>
    /// <para lang="zh">更新值方法</para>
    /// <para lang="en">Update Value Method</para>
    /// </summary>
    /// <param name="d"></param>
    private async Task UpdateValue(DateTime d)
    {
        if (SelectedValue.Start == DateTime.MinValue)
        {
            // 开始时间为空
            SelectedValue.Start = ViewMode == DatePickerViewMode.DateTime ? d.Date + StartValue.TimeOfDay : d;
        }
        else if (SelectedValue.End == DateTime.MinValue)
        {
            // 结束时间为空
            if (ViewMode == DatePickerViewMode.DateTime ? d.Date < SelectedValue.Start.Date : d < SelectedValue.Start)
            {
                SelectedValue.End = ViewMode == DatePickerViewMode.DateTime
                    ? SelectedValue.Start.Date + EndValue.TimeOfDay
                    : GetEndDateTime(SelectedValue.Start);
                SelectedValue.Start = ViewMode == DatePickerViewMode.DateTime ? d.Date + StartValue.TimeOfDay : d;
            }
            else
            {
                SelectedValue.End = ViewMode == DatePickerViewMode.DateTime ? d.Date + EndValue.TimeOfDay : d;
            }
        }
        else
        {
            // 开始时间、结束时间均不为空
            SelectedValue.Start = ViewMode == DatePickerViewMode.DateTime ? d.Date + StartValue.TimeOfDay : d;
            SelectedValue.End = DateTime.MinValue;
        }

        if (ViewMode is DatePickerViewMode.Year or DatePickerViewMode.Month)
        {
            if (SelectedValue.Start != DateTime.MinValue)
            {
                StartValue = SelectedValue.Start;
            }
            if (SelectedValue.End != DateTime.MinValue)
            {
                EndValue = SelectedValue.End;
            }
        }

        if (ShowSelectedValue)
        {
            Value.Start = SelectedValue.Start;
            Value.End = SelectedValue.End;
        }

        if (OnDateClick != null)
        {
            await OnDateClick(d);
        }

        if (AutoClose && ViewMode != DatePickerViewMode.DateTime && SelectedValue.Start != DateTime.MinValue && SelectedValue.End != DateTime.MinValue)
        {
            await ClickConfirmButton();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyValue"></param>
    public override bool IsComplexValue(object? propertyValue) => false;

    private static DateTime GetEndDateTime(DateTime dt) => dt.Date.Add(_endTimeValue);

    private void ResetBodyValue()
    {
        _activeTimePicker = null;
        Value ??= new DateTimeRangeValue();
        EndValue = Value.End == DateTime.MinValue ? GetEndDateTime(DateTime.Today) : Value.End;

        SelectedValue.Start = Value.Start;
        SelectedValue.End = Value.End;

        if (ViewMode == DatePickerViewMode.Year)
        {
            var d = DateTime.Today.AddYears(-1);
            StartValue = Value.Start == DateTime.MinValue ? new DateTime(d.Year, 1, 1) : Value.Start;
        }
        else if (ViewMode == DatePickerViewMode.Month)
        {
            var d = DateTime.Today.AddMonths(-1);
            StartValue = Value.Start == DateTime.MinValue ? new DateTime(d.Year, d.Month, 1) : Value.Start;
        }
        else if (ViewMode == DatePickerViewMode.DateTime)
        {
            StartValue = EndValue.GetSafeMonthDateTime(-1).Date
                + (Value.Start == DateTime.MinValue ? _startTimeValue : Value.Start.TimeOfDay);
        }
        else
        {
            StartValue = EndValue.GetSafeMonthDateTime(-1).Date;
        }
    }

    /// <summary>
    /// <para lang="zh">客户端弹窗关闭后由 Javascript 调用此方法</para>
    /// <para lang="en">Method Called by Javascript After Client Popup Closed</para>
    /// </summary>
    [JSInvokable]
    public Task TriggerHideCallback()
    {
        ResetBodyValue();
        StateHasChanged();
        return Task.CompletedTask;
    }
}
