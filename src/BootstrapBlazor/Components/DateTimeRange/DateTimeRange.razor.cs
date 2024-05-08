// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimeRange 时间范围组件
/// </summary>
public partial class DateTimeRange
{
    /// <summary>
    /// 获得 组件样式名称
    /// </summary>
    private string? ClassString => CssBuilder.Default("select datetime-range form-control")
        .AddClass("disabled", IsDisabled)
        .AddClass("has-time", ViewMode == DatePickerViewMode.DateTime)
        .AddClass(ValidCss)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 组件小图标样式
    /// </summary>
    private string? DateTimePickerIconClassString => CssBuilder.Default("range-bar")
        .AddClass(Icon)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// 获得 用户选中的时间范围
    /// </summary>
    internal DateTimeRangeValue SelectedValue { get; } = new DateTimeRangeValue();

    private DateTime StartValue { get; set; }

    private string? StartValueString
    {
        set
        {
            var format = ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat;
            var ret = DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDateValue);
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
            var ret = DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDateValue);
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

    [NotNull]
    private string? StartPlaceHolderText { get; set; }

    [NotNull]
    private string? EndPlaceHolderText { get; set; }

    [NotNull]
    private string? SeparateText { get; set; }

    /// <summary>
    /// 获得/设置 是否可以编辑内容 默认 false
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// 获得/设置 是否点击快捷侧边栏自动关闭弹窗 默认 false
    /// </summary>
    [Parameter]
    public bool AutoCloseClickSideBar { get; set; }

    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 清空按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 获得/设置 清空图标 默认 fa-solid fa-circle-xmark
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    [Parameter]
    public DatePickerViewMode ViewMode { get; set; } = DatePickerViewMode.Date;

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    [Parameter]
    public DateTimeRangeRenderMode RenderMode { get; set; } = DateTimeRangeRenderMode.Double;

    /// <summary>
    /// 获得/设置 今天按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TodayButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确定按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 最大值
    /// </summary>
    [Parameter]
    public DateTime MaxValue { get; set; } = DateTime.MaxValue;
    /// <summary>
    /// 获得/设置 最小值
    /// </summary>
    [Parameter]
    public DateTime MinValue { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 获得/设置 是否允许为空 默认为 true
    /// </summary>
    [Parameter]
    [Obsolete("已过期，请使用 ShowClearButton 代替")]
    [ExcludeFromCodeCoverage]
    public bool AllowNull
    {
        get => ShowClearButton;
        set => ShowClearButton = value;
    }

    /// <summary>
    /// 获得/设置 是否显示清空按钮 默认 true
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示今天按钮 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowToday { get; set; }

    /// <summary>
    /// 获得/设置 是否显示快捷侧边栏 默认不显示
    /// </summary>
    [Parameter]
    public bool ShowSidebar { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏快捷选项集合
    /// </summary>
    [Parameter]
    [NotNull]
    public List<DateTimeRangeSidebarItem>? SidebarItems { get; set; }

    /// <summary>
    /// 点击确认按钮回调委托方法
    /// </summary>
    [Parameter]
    public Func<DateTimeRangeValue, Task>? OnConfirm { get; set; }

    /// <summary>
    /// 点击清空按钮回调委托方法
    /// </summary>
    [Parameter]
    public Func<DateTimeRangeValue, Task>? OnClearValue { get; set; }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 "HH:mm:ss"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd HH:mm:ss"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateTimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国阴历历法 默认 false
    /// </summary>
    [Parameter]
    public bool ShowLunar { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国 24 节气 默认 false
    /// </summary>
    [Parameter]
    public bool ShowSolarTerm { get; set; }

    /// <summary>
    /// 获得/设置 是否显示节日 默认 false
    /// </summary>
    [Parameter]
    public bool ShowFestivals { get; set; }

    /// <summary>
    /// 获得/设置 是否显示休假日 默认 false
    /// </summary>
    [Parameter]
    public bool ShowHolidays { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimeRange>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizerFactory? LocalizerFactory { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ReadonlyString => IsEditable ? null : "readonly";

    private string? ValueClassString => CssBuilder.Default("datetime-range-input")
        .AddClass("datetime", ViewMode == DatePickerViewMode.DateTime)
        .AddClass("disabled", IsDisabled)
        .Build();

    private bool _showRightButtons = false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (EditContext != null && FieldIdentifier != null)
        {
            var pi = FieldIdentifier.Value.Model.GetType().GetPropertyByName(FieldIdentifier.Value.FieldName);
            if (pi != null)
            {
                var required = pi.GetCustomAttribute<RequiredAttribute>(true);
                if (required != null)
                {
                    Rules.Add(new DateTimeRangeRequiredValidator()
                    {
                        LocalizerFactory = LocalizerFactory,
                        ErrorMessage = required.ErrorMessage,
                        AllowEmptyString = required.AllowEmptyStrings
                    });
                }
            }
        }

        _showRightButtons = RenderMode == DateTimeRangeRenderMode.Single;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        CheckValid();

        base.OnParametersSet();

        StartPlaceHolderText ??= Localizer[nameof(StartPlaceHolderText)];
        EndPlaceHolderText ??= Localizer[nameof(EndPlaceHolderText)];
        SeparateText ??= Localizer[nameof(SeparateText)];

        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        TodayButtonText ??= Localizer[nameof(TodayButtonText)];

        DateFormat ??= Localizer[nameof(DateFormat)];
        DateTimeFormat ??= Localizer[nameof(DateTimeFormat)];

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.DateTimeRangeIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DateTimeRangeClearIcon); ;

        SidebarItems ??=
        [
            new() { Text = Localizer["Last7Days"], StartDateTime = DateTime.Today.AddDays(-7), EndDateTime = DateTime.Today.AddDays(1).AddSeconds(-1) },
            new() { Text = Localizer["Last30Days"], StartDateTime = DateTime.Today.AddDays(-30), EndDateTime = DateTime.Today.AddDays(1).AddSeconds(-1) },
            new() { Text = Localizer["ThisMonth"], StartDateTime = DateTime.Today.AddDays(1 - DateTime.Today.Day), EndDateTime = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(1).AddSeconds(-1) },
            new() { Text = Localizer["LastMonth"], StartDateTime = DateTime.Today.AddDays(1- DateTime.Today.Day).AddMonths(-1), EndDateTime = DateTime.Today.AddDays(1- DateTime.Today.Day).AddSeconds(-1) },
        ];

        Value ??= new DateTimeRangeValue();

        EndValue = Value.End == DateTime.MinValue ? GetEndDateTime(DateTime.Today) : Value.End;
        StartValue = EndValue.AddMonths(-1).Date;

        SelectedValue.Start = Value.Start;
        SelectedValue.End = Value.End;

        [ExcludeFromCodeCoverage]
        void CheckValid()
        {
            if (ViewMode == DatePickerViewMode.DateTime)
            {
                throw new InvalidOperationException("DateTime 模式暂时不支持，The DateTime mode is currently not supported yet");
            }
        }
    }

    private async Task OnClickSidebarItem(DateTimeRangeSidebarItem item)
    {
        SelectedValue.Start = item.StartDateTime;
        SelectedValue.End = item.EndDateTime;

        StartValue = item.StartDateTime;
        EndValue = StartValue.GetSafeMonthDateTime(1).Date + SelectedValue.End.TimeOfDay;

        if (AutoCloseClickSideBar)
        {
            await InvokeVoidAsync("hide", Id);
            await ClickConfirmButton();
        }
    }

    /// <summary>
    /// 点击 清除按钮调用此方法
    /// </summary>
    /// <returns></returns>
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
    }

    private Task OnStartDateChanged(DateTime value)
    {
        StartValue = value.Date + StartValue.TimeOfDay;
        EndValue = GetEndDateTime(StartValue.AddMonths(1).Date);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnEndDateChanged(DateTime value)
    {
        EndValue = GetEndDateTime(value);
        StartValue = value.AddMonths(-1).Date + StartValue.TimeOfDay;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 点击 确认时调用此方法
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
    /// 点击 确认时调用此方法
    /// </summary>
    private async Task ClickConfirmButton()
    {
        // SelectedValue 
        if (SelectedValue.End == DateTime.MinValue)
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
        Value.Start = SelectedValue.Start.Date;
        Value.End = GetEndDateTime(SelectedValue.End);

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
    }

    /// <summary>
    /// 更新值方法
    /// </summary>
    /// <param name="d"></param>
    private void UpdateValue(DateTime d)
    {
        if (SelectedValue.Start == DateTime.MinValue)
        {
            // 开始时间为空
            SelectedValue.Start = d;
        }
        else if (SelectedValue.End == DateTime.MinValue)
        {
            // 结束时间为空
            if (d < SelectedValue.Start)
            {
                SelectedValue.End = SelectedValue.Start;
                SelectedValue.Start = d;
            }
            else
            {
                SelectedValue.End = d;
            }
        }
        else
        {
            // 开始时间、结束时间均不为空
            SelectedValue.Start = d;
            SelectedValue.End = DateTime.MinValue;
        }
        //StartValue = SelectedValue.Start;
        //EndValue = SelectedValue.End;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    public override bool IsComplexValue(object? propertyValue) => false;

    private static DateTime GetEndDateTime(DateTime dt) => dt.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

    private DateTime GetSafeStartValue() => SelectedValue.Start.Date == SelectedValue.End.Date ? SelectedValue.Start.GetSafeMonthDateTime(-1) : SelectedValue.Start.Date;
}
