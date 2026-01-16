// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">CheckboxList 组件基类</para>
/// <para lang="en">CheckboxList component base class</para>
/// </summary>
public partial class CheckboxListGeneric<TValue> : IModelEqualityComparer<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 组件样式</para>
    /// <para lang="en">Get component style</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("checkbox-list form-control")
        .AddClass("no-border", !ShowBorder && ValidCss != "is-invalid")
        .AddClass("is-vertical", IsVertical)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 组件内部 Checkbox 项目样式</para>
    /// <para lang="en">Get the Checkbox item style inside the component</para>
    /// </summary>
    private string? CheckboxItemClassString => CssBuilder.Default("checkbox-item")
        .AddClass(CheckboxItemClass)
        .Build();

    private string? ButtonClassString => CssBuilder.Default("checkbox-list is-button")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ButtonGroupClassString => CssBuilder.Default("btn-group")
        .AddClass("disabled", IsDisabled)
        .AddClass("btn-group-vertical", IsVertical)
        .Build();

    private string? GetButtonItemClassString(SelectedItem<TValue> item) => CssBuilder.Default("btn")
        .AddClass($"active bg-{Color.ToDescriptionString()}", IsEquals(item.Value))
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 数据主键标识标签 默认为 <see cref="KeyAttribute"/><code><br /></code>用于判断数据主键标签，如果模型未设置主键时可使用 <see cref="ModelEqualityComparer"/> 参数自定义判断 <code><br /></code>数据模型支持联合主键</para>
    /// <para lang="en">Gets or sets the data primary key attribute tag. Default is <see cref="KeyAttribute"/><code><br /></code>Used to judge the data primary key tag. If the model does not set the primary key, you can use the <see cref="ModelEqualityComparer"/> parameter to customize the judgment <code><br /></code>Data model supports joint primary keys</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// <para lang="zh">获得/设置 比较数据是否相同回调方法 默认为 null</para>
    /// <para lang="en">Gets or sets the callback method to compare whether the data is the same. Default is null</para>
    /// <para lang="zh">提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</para>
    /// <para lang="en">Ignore the <see cref="CustomKeyAttribute"/> property when providing this callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TValue, TValue, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据源</para>
    /// <para lang="en">Gets or sets the data source</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem<TValue>>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为按钮样式 默认 false</para>
    /// <para lang="en">Gets or sets whether it is a button style. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Checkbox 组件布局样式</para>
    /// <para lang="en">Gets or sets the Checkbox component layout style</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CheckboxItemClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示边框 默认为 true</para>
    /// <para lang="en">Gets or sets whether to show the border. Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowBorder { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否为竖向排列 默认为 false</para>
    /// <para lang="en">Gets or sets whether to arrange vertically. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮颜色 默认为 None 未设置</para>
    /// <para lang="en">Gets or sets the button color. Default is None (not set)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 SelectedItemChanged 方法</para>
    /// <para lang="en">Gets or sets the SelectedItemChanged method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem<TValue>>, List<TValue>, Task>? OnSelectedChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最多选中数量</para>
    /// <para lang="en">Gets or sets the maximum number of selected items</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int MaxSelectedCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 超过最大选中数量时回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate when the maximum number of selected items is exceeded</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnMaxSelectedCountExceed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 项模板</para>
    /// <para lang="en">Gets or sets the item template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem<TValue>>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得 当前选项是否被禁用</para>
    /// <para lang="en">Get whether the current option is disabled</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected bool GetDisabledState(SelectedItem<TValue> item) => IsDisabled || item.IsDisabled;

    private Func<CheckboxState, Task<bool>>? _onBeforeStateChangedCallback;

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // <para lang="zh">处理 Required 标签</para>
        // <para lang="en">Process Required attribute</para>
        AddRequiredValidator();
    }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsButton && Color == Color.None)
        {
            Color = Color.Primary;
        }

        Items ??= [];

        _onBeforeStateChangedCallback = MaxSelectedCount > 0 ? new Func<CheckboxState, Task<bool>>(OnBeforeStateChanged) : null;

        // <para lang="zh">set item active</para>
        // <para lang="en">set item active</para>
        if (Value != null)
        {
            var item = Items.FirstOrDefault(i => IsEquals(i.Value));
            if (item != null)
            {
                item.Active = true;
            }
        }
    }

    private bool IsEquals(TValue? val) => Value != null && Value.Find(v => Equals(v, val)) != null;

    private async Task<bool> OnBeforeStateChanged(CheckboxState state)
    {
        var ret = true;
        if (state == CheckboxState.Checked)
        {
            var items = Items.Where(i => i.Active).ToList();
            ret = items.Count < MaxSelectedCount;
        }

        if (!ret && OnMaxSelectedCountExceed != null)
        {
            await OnMaxSelectedCountExceed();
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">Checkbox 组件选项状态改变时触发此方法</para>
    /// <para lang="en">Trigger this method when the Checkbox component option state changes</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="v"></param>
    private async Task OnStateChanged(SelectedItem<TValue> item, bool v)
    {
        item.Active = v;
        var items = Items.Where(i => i.Active).Select(i => i.Value).ToList();
        CurrentValue = items;

        if (OnSelectedChanged != null)
        {
            await OnSelectedChanged(Items, CurrentValue);
        }
        else
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">点击选择框方法</para>
    /// <para lang="en">Click checkbox method</para>
    /// </summary>
    private Task OnClick(SelectedItem<TValue> item) => OnStateChanged(item, !item.Active);

    private RenderFragment? GetChildContent(SelectedItem<TValue> item) => ItemTemplate == null
        ? null
        : ItemTemplate(item);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Equals(TValue? x, TValue? y) => this.Equals<TValue>(x, y);
}
