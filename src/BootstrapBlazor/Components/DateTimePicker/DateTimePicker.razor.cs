// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimePicker 组件
/// </summary>
public partial class DateTimePicker<TValue>
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
        .AddClass("has-icon", ShowIcon)
        .AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 组件小图标样式
    /// </summary>
    private string? DateTimePickerIconClassString => CssBuilder.Default("datetime-picker-bar")
        .AddClass(Icon)
        .Build();

    private string? TabIndexString => ValidateForm != null ? "0" : null;

    /// <summary>
    /// 获得 Placeholder 显示字符串
    /// </summary>
    private string? PlaceholderString => ViewMode switch
    {
        DatePickerViewMode.DateTime => DateTimePlaceHolderText,
        _ => DatePlaceHolderText
    };

    /// <summary>
    /// 获得/设置 是否允许为空
    /// </summary>
    private bool AllowNull { get; set; }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 null
    /// </summary>
    [Parameter]
    [Obsolete("已过期，请使用 DateTimeFormat/DateFormat/TimeFormat 分别设置; Please use DateTimeFormat/DateFormat/TimeFormat")]
    [ExcludeFromCodeCoverage]
    public string? Format
    {
        get => DateFormat;
        set => DateFormat = value;
    }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd HH:mm:ss"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateTimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 "yyyy-MM-dd"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 获得/设置 时间格式化字符串 默认值为 "HH:mm:ss"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 星期第一天 默认 <see cref="DayOfWeek.Sunday"/>
    /// </summary>
    [Parameter]
    public DayOfWeek FirstDayOfWeek { get; set; } = DayOfWeek.Sunday;

    /// <summary>
    /// 获得/设置 组件图标 默认 fa-regular fa-calendar-days
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示组件图标 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; } = true;

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
    /// 获得/设置 侧边栏模板 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment<Func<DateTime, Task>>? SidebarTemplate { get; set; }

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
    /// 获得/设置 是否点击日期后自动关闭弹窗 默认 true
    /// </summary>
    [Parameter]
    public bool AutoClose { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否可以编辑内容 默认 false
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// 获得/设置 是否自动设置值为当前时间 默认 true
    /// </summary>
    /// <remarks>当 Value 值为 <see cref="DateTime.MinValue"/> 时自动设置时间为 <see cref="DateTime.Today"/> 不为空类型时此参数生效</remarks>
    [Parameter]
    public bool AutoToday { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否将 <see cref="DateTime.MinValue"/> 显示为空字符串 默认 true
    /// </summary>
    /// <remarks>可为空类型时此参数生效</remarks>
    [Parameter]
    public bool DisplayMinValueAsEmpty { get; set; } = true;

    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 日期占位符文本 默认 null 读取资源文件
    /// </summary>
    [Parameter]
    public string? DatePlaceHolderText { get; set; }

    /// <summary>
    /// 获得/设置 日期时间占位符文本 默认 null 读取资源文件
    /// </summary>
    [Parameter]
    public string? DateTimePlaceHolderText { get; set; }

    /// <summary>
    /// 获得/设置 日单元格模板
    /// </summary>
    [Parameter]
    public RenderFragment<DateTime>? DayTemplate { get; set; }

    /// <summary>
    /// 获得/设置 禁用日单元格模板
    /// </summary>
    [Parameter]
    public RenderFragment<DateTime>? DayDisabledTemplate { get; set; }

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

    /// <summary>
    /// 获取/设置 获得自定义禁用日期回调方法，默认 null 内部默认启用数据缓存 可通过 <see cref="EnableDisabledDaysCache"/> 参数关闭
    /// </summary>
    [Parameter]
    public Func<DateTime, DateTime, Task<List<DateTime>>>? OnGetDisabledDaysCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否启用获得年自定义禁用日期缓存
    /// </summary>
    [Parameter]
    public bool EnableDisabledDaysCache { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否将禁用日期显示为空字符串 默认 false 开启后组件会频繁调用 <see cref="OnGetDisabledDaysCallback"/> 方法，建议外部使用缓存提高性能
    /// </summary>
    [Parameter]
    public bool DisplayDisabledDayAsEmpty { get; set; }

    /// <summary>
    /// 获得/设置 失去焦点回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnBlurAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [NotNull]
    private string? GenericTypeErrorMessage { get; set; }

    private DateTime SelectedValue { get; set; }

    private DatePickerBody _pickerBody = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 泛型设置为可为空
        AllowNull = NullableUnderlyingType != null;
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DateTimePlaceHolderText ??= Localizer[nameof(DateTimePlaceHolderText)];
        DatePlaceHolderText ??= Localizer[nameof(DatePlaceHolderText)];
        GenericTypeErrorMessage ??= Localizer[nameof(GenericTypeErrorMessage)];
        DateTimeFormat ??= Localizer[nameof(DateTimeFormat)];
        DateFormat ??= Localizer[nameof(DateFormat)];
        TimeFormat ??= Localizer[nameof(TimeFormat)];

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.DateTimePickerIcon);

        var type = typeof(TValue);

        // 判断泛型类型
        if (!type.IsDateTime())
        {
            throw new InvalidOperationException(GenericTypeErrorMessage);
        }

        if (Value is DateTimeOffset v1)
        {
            SelectedValue = v1.DateTime;
        }
        else
        {
            SelectedValue = Value == null ? DateTime.MinValue : (DateTime)(object)Value;
        }

        if (MinValue > SelectedValue)
        {
            SelectedValue = ViewMode == DatePickerViewMode.DateTime ? MinValue.Value : MinValue.Value.Date;
            Value = GetValue();
        }
        else if (MaxValue < SelectedValue)
        {
            SelectedValue = ViewMode == DatePickerViewMode.DateTime ? MaxValue.Value : MaxValue.Value.Date;
            Value = GetValue();
        }

        if (MinValueToEmpty(SelectedValue))
        {
            SelectedValue = ViewMode == DatePickerViewMode.DateTime ? DateTime.Now : DateTime.Today;
            Value = default;
        }
        else if (MinValueToToday(SelectedValue))
        {
            SelectedValue = ViewMode == DatePickerViewMode.DateTime ? DateTime.Now : DateTime.Today;
            Value = GetValue();
        }
    }

    private List<DateTime> _disabledDaysList = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (OnGetDisabledDaysCallback != null && DisplayDisabledDayAsEmpty)
        {
            DateTime d = Value switch
            {
                DateTime v1 => v1,
                DateTimeOffset v2 => v2.DateTime,
                _ => DateTime.MinValue
            };

            if (d != DateTime.MinValue)
            {
                _render = false;
                _disabledDaysList = await OnGetDisabledDaysCallback(d, d);
                _render = true;
            }
        }
    }

    private bool _render = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override bool ShouldRender() => _render;

    /// <summary>
    /// 格式化数值方法
    /// </summary>
    protected override string FormatValueAsString(TValue? value)
    {
        var ret = "";
        DateTime? d = value switch
        {
            DateTime v1 => v1,
            DateTimeOffset v2 => v2.DateTime,
            _ => null
        };

        if (d.HasValue && !_disabledDaysList.Contains(d.Value))
        {
            ret = d.Value.ToString(ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat);
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new
    {
        TriggerHideCallback = nameof(TriggerHideCallback)
    });

    private bool MinValueToEmpty(DateTime val) => val == DateTime.MinValue && AllowNull && DisplayMinValueAsEmpty;

    private bool MinValueToToday(DateTime val) => val == DateTime.MinValue && !AllowNull && AutoToday;

    /// <summary>
    /// 清除内部缓存方法
    /// </summary>
    public void ClearDisabledDays() => _pickerBody.ClearDisabledDays();

    /// <summary>
    /// 确认按钮点击时回调此方法
    /// </summary>
    private async Task OnConfirm()
    {
        CurrentValue = GetValue()!;

        if (AutoClose)
        {
            await InvokeVoidAsync("hide", Id);
        }
    }

    private async Task OnClear()
    {
        // 允许为空时才会触发 OnClear 方法
        CurrentValue = default!;
        SelectedValue = DateTime.Today;

        if (AutoClose)
        {
            await InvokeVoidAsync("hide", Id);
        }
    }

    private TValue? GetValue()
    {
        TValue? ret = default;

        if (ValueType == typeof(DateTime))
        {
            ret = (TValue)(object)SelectedValue;
        }
        else if (ValueType == typeof(DateTimeOffset))
        {
            DateTimeOffset d = new DateTimeOffset(SelectedValue);
            ret = (TValue)(object)d;
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        var format = ViewMode == DatePickerViewMode.DateTime ? DateTimeFormat : DateFormat;
        result = default;
        validationErrorMessage = null;
        var ret = DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var val);
        if (ret)
        {
            result = (TValue)(object)val;
        }
        return ret;
    }

    private string? ReadonlyString => IsEditable ? null : "readonly";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected virtual async Task OnBlur()
    {
        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }

    /// <summary>
    /// 客户端弹窗关闭后由 Javascript 调用此方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task TriggerHideCallback()
    {
        StateHasChanged();
        return Task.CompletedTask;
    }
}
