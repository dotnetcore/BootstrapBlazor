// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 单选框组合组件
/// </summary>
[ExcludeFromCodeCoverage]
public partial class RadioListGeneric<TValue>
{
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
    public RenderFragment<SelectedItem<TValue>>? ItemTemplate { get; set; }

    //private string? GroupName => Id;

    //private string? ClassString => CssBuilder.Default("radio-list form-control")
    //    .AddClass("no-border", !ShowBorder && ValidCss != "is-invalid")
    //    .AddClass("is-vertical", IsVertical)
    //    .AddClass(CssClass).AddClass(ValidCss)
    //    .Build();

    //private string? ButtonClassString => CssBuilder.Default("radio-list btn-group")
    //    .AddClass("disabled", IsDisabled)
    //    .AddClass("btn-group-vertical", IsVertical)
    //    .AddClassFromAttributes(AdditionalAttributes)
    //    .Build();

    //private string? GetButtonItemClassString(SelectedItem item) => CssBuilder.Default("btn")
    //    .AddClass($"active bg-{Color.ToDescriptionString()}", CurrentValueAsString == item.Value)
    //    .Build();

    ///// <summary>
    ///// <inheritdoc/>
    ///// </summary>
    //protected override void OnParametersSet()
    //{
    //    var t = NullableUnderlyingType ?? typeof(TValue);
    //    if (t.IsEnum && Items == null)
    //    {
    //        Items = t.ToSelectList<TValue>((NullableUnderlyingType != null && IsAutoAddNullItem) ? new SelectedItem<TValue>(default, NullItemText) : null);
    //    }

    //    base.OnParametersSet();
    //}

    ///// <summary>
    ///// 点击选择框方法
    ///// </summary>
    //private async Task OnClick(SelectedItem<TValue> item)
    //{
    //    if (!IsDisabled)
    //    {
    //        if (OnSelectedChanged != null)
    //        {
    //            await OnSelectedChanged(Items, Value);
    //        }
    //        StateHasChanged();
    //    }
    //}

    //private CheckboxState CheckState(SelectedItem<TValue> item) => this.Equals<TValue>(Value, item.Value) ? CheckboxState.Checked : CheckboxState.UnChecked;

    //private RenderFragment? GetChildContent(SelectedItem<TValue> item) => ItemTemplate == null
    //    ? null
    //    : ItemTemplate(item);
}
