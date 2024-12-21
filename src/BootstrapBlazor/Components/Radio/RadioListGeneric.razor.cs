// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 单选框组合组件
/// </summary>
public partial class RadioListGeneric<TValue> : IModelEqualityComparer<TValue>
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

    /// <summary>
    /// 获得/设置 是否为按钮样式 默认 false
    /// </summary>
    [Parameter]
    public bool IsButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示边框 默认为 true
    /// </summary>
    [Parameter]
    public bool ShowBorder { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否为竖向排列 默认为 false
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色 默认为 None 未设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem<TValue>>? Items { get; set; }

    /// <summary>
    /// 获得/设置 SelectedItemChanged 方法
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnSelectedChanged { get; set; }

    /// <summary>
    /// 获得/设置 数据主键标识标签 默认为 <see cref="KeyAttribute"/><code><br /></code>用于判断数据主键标签，如果模型未设置主键时可使用 <see cref="ModelEqualityComparer"/> 参数自定义判断 <code><br /></code>数据模型支持联合主键
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// 获得/设置 比较数据是否相同回调方法 默认为 null
    /// <para>提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</para>
    /// </summary>
    [Parameter]
    public Func<TValue, TValue, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// 获得 当前选项是否被禁用
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool GetDisabledState(SelectedItem<TValue> item) => IsDisabled || item.IsDisabled;

    private string? GroupName => Id;

    private string? ClassString => CssBuilder.Default("radio-list form-control")
        .AddClass("no-border", !ShowBorder && ValidCss != "is-invalid")
        .AddClass("is-vertical", IsVertical)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? ButtonClassString => CssBuilder.Default("radio-list btn-group")
        .AddClass("disabled", IsDisabled)
        .AddClass("btn-group-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetButtonItemClassString(SelectedItem<TValue> item) => CssBuilder.Default("btn")
        .AddClass($"active bg-{Color.ToDescriptionString()}", Equals(Value, item.Value))
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsButton && Color == Color.None)
        {
            Color = Color.Primary;
        }

        var t = NullableUnderlyingType ?? typeof(TValue);
        if (t.IsEnum && Items == null)
        {
            Items = t.ToSelectList<TValue>((NullableUnderlyingType != null && IsAutoAddNullItem) ? new SelectedItem<TValue>(default!, NullItemText) : null);
        }

        Items ??= [];

        // set item active
        if (Value != null)
        {
            var item = Items.FirstOrDefault(i => Equals(Value, i.Value));
            if (item == null)
            {
                Value = default;
            }
        }
    }

    /// <summary>
    /// 点击选择框方法
    /// </summary>
    private async Task OnClick(SelectedItem<TValue> item)
    {
        if (!IsDisabled)
        {
            CurrentValue = item.Value;
            if (OnSelectedChanged != null)
            {
                await OnSelectedChanged(Value);
            }
            StateHasChanged();
        }
    }

    private CheckboxState CheckState(SelectedItem<TValue> item) => this.Equals<TValue>(Value, item.Value) ? CheckboxState.Checked : CheckboxState.UnChecked;

    private RenderFragment? GetChildContent(SelectedItem<TValue> item) => ItemTemplate == null
        ? null
        : ItemTemplate(item);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Equals(TValue? x, TValue? y) => this.Equals<TValue>(x, y);
}
