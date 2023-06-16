// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections;

namespace BootstrapBlazor.Components;

/// <summary>
/// 单选框组合组件
/// </summary>
public partial class RadioList<TValue> : CheckboxList<TValue>
{
    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 值为可为空枚举类型时是否自动添加空值 默认 false 自定义空值显示文本请参考 <see cref="NullItemText"/>
    /// </summary>
    [Parameter]
    public bool IsAutoAddNullItem { get; set; }

    /// <summary>
    /// 获得/设置 空值项显示文字 默认为 "" 是否自动添加空值请参考 <see cref="IsAutoAddNullItem"/>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NullItemText { get; set; }

    /// <summary>
    /// 获得/设置 项模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 未设置选中项时是否自动选择第一项 默认 true
    /// </summary>
    [Parameter]
    public bool AutoSelectFirstWhenValueIsNull { get; set; } = true;

    private string? GroupName => Id;

    private string? RadioClassString => CssBuilder.Default("radio-list")
        .AddClass("form-control", !IsButton)
        .AddClass("is-button", IsButton)
        .Build();

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        var t = NullableUnderlyingType ?? typeof(TValue);
        if (t.IsEnum && Items == null)
        {
            Items = t.ToSelectList((NullableUnderlyingType != null && IsAutoAddNullItem) ? new SelectedItem("", NullItemText) : null);
        }

        base.OnParametersSet();

        NullItemText ??= "";

        if (AutoSelectFirstWhenValueIsNull && !Items.Any(i => i.Value == CurrentValueAsString))
        {
            CurrentValueAsString = Items.FirstOrDefault()?.Value ?? "";
        }
    }

    /// <summary>
    /// 格式化 Value 方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(TValue value) => value is SelectedItem v ? v.Value : base.FormatValueAsString(value);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        var ret = false;
        var t = NullableUnderlyingType ?? typeof(TValue);
        result = default;
        if (t == typeof(SelectedItem))
        {
            var item = Items.FirstOrDefault(i => i.Value == value);
            if (item != null)
            {
                result = (TValue)(object)item;
                ret = true;
            }
        }
        validationErrorMessage = null;
        return ret || base.TryParseValueFromString(value, out result, out validationErrorMessage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeValue"></param>
    /// <param name="list"></param>
    protected override void ProcessGenericItems(Type typeValue, IEnumerable? list) { }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void EnsureParameterValid() { }

    /// <summary>
    /// 点击选择框方法
    /// </summary>
    private async Task OnClick(SelectedItem item)
    {
        if (!IsDisabled)
        {
            if (typeof(TValue) == typeof(SelectedItem))
            {
                CurrentValue = (TValue)(object)item;
            }
            else
            {
                CurrentValueAsString = item.Value;
            }
            if (OnSelectedChanged != null)
            {
                await OnSelectedChanged.Invoke(new SelectedItem[] { item }, Value);
            }

            StateHasChanged();
        }
    }

    private CheckboxState CheckState(SelectedItem item) => item.Value == CurrentValueAsString ? CheckboxState.Checked : CheckboxState.UnChecked;

    private RenderFragment? GetChildContent(SelectedItem item) => ItemTemplate == null
        ? null
        : ItemTemplate(item);
}
