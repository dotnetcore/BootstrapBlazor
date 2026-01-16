// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">CheckboxList 组件基类</para>
/// <para lang="en">CheckboxList component base class</para>
/// </summary>
public partial class CheckboxList<TValue> : ValidateBase<TValue>
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
    protected string? CheckboxItemClassString => CssBuilder.Default("checkbox-item")
        .AddClass(CheckboxItemClass)
        .Build();

    private string? ButtonClassString => CssBuilder.Default("checkbox-list is-button")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ButtonGroupClassString => CssBuilder.Default("btn-group")
        .AddClass("disabled", IsDisabled)
        .AddClass("btn-group-vertical", IsVertical)
        .Build();

    private string? GetButtonItemClassString(SelectedItem item) => CssBuilder.Default("btn")
        .AddClass($"border-secondary", !ShowButtonBorderColor)
        .AddClass($"border-{Color.ToDescriptionString()}", ShowButtonBorderColor)
        .AddClass($"active bg-{Color.ToDescriptionString()}", CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries).Contains(item.Value))
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 数据源</para>
    /// <para lang="en">Gets or sets the data source</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为按钮样式 默认 false</para>
    /// <para lang="en">Gets or sets whether it is a button style. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置  是否显示按钮边框颜色 默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the button border color. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowButtonBorderColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Checkbox 组件布局样式</para>
    /// <para lang="en">Gets or sets the Checkbox component layout style</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CheckboxItemClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 非按钮模式下是否显示组件边框 默认为 true</para>
    /// <para lang="en">Gets or sets whether to show the component border in non-button mode. Default is true</para>
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
    public Func<IEnumerable<SelectedItem>, TValue, Task>? OnSelectedChanged { get; set; }

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
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得 当前选项是否被禁用</para>
    /// <para lang="en">Get whether the current option is disabled</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected bool GetDisabledState(SelectedItem item) => IsDisabled || item.IsDisabled;

    private Func<CheckboxState, Task<bool>>? _onBeforeStateChangedCallback;

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        EnsureParameterValid();

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

        if (Items == null)
        {
            var t = typeof(TValue);
            var innerType = t.GetGenericArguments().FirstOrDefault();
            if (innerType != null)
            {
                Items = innerType.ToSelectList();
            }
            Items ??= [];
        }

        InitValue();

        _onBeforeStateChangedCallback = MaxSelectedCount > 0 ? new Func<CheckboxState, Task<bool>>(OnBeforeStateChanged) : null;
    }
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
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(TValue? value)
    {
        string? ret = null;
        if (ValueType == typeof(string))
        {
            ret = value?.ToString();
        }
        else
        {
            ret = string.Join(",", Items.Where(i => i.Active).Select(i => i.Value));
        }
        return ret;
    }

    private void InitValue()
    {
        // <para lang="zh">通过 Value 对集合进行赋值</para>
        // <para lang="en">Assign value to collection via Value</para>
        if (Value != null)
        {
            var typeValue = typeof(TValue);
            IEnumerable? list = null;
            if (typeValue == typeof(string))
            {
                var values = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in Items)
                {
                    item.Active = values.Any(v => v.Equals(item.Value, StringComparison.OrdinalIgnoreCase));
                }
                list = values;
            }
            else if (typeValue.IsGenericType)
            {
                ProcessGenericItems(typeValue, list);
            }
        }
    }

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    /// <param name="typeValue"></param>
    /// <param name="list"></param>
    protected virtual void ProcessGenericItems(Type typeValue, IEnumerable? list)
    {
        var t = typeValue.GenericTypeArguments;
        var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t));
        if (instance != null)
        {
            var mi = instance.GetType().GetMethod(nameof(List<string>.AddRange))!;
            mi.Invoke(instance, [Value]);
            if (instance is IEnumerable l)
            {
                list = l;
                foreach (var item in Items)
                {
                    item.Active = false;
                    foreach (var v in list)
                    {
                        item.Active = item.Value.Equals(v!.ToString(), StringComparison.OrdinalIgnoreCase);
                        if (item.Active)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// <para lang="zh">Checkbox 组件选项状态改变时触发此方法</para>
    /// <para lang="en">Trigger this method when the Checkbox component option state changes</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="v"></param>
    private async Task OnStateChanged(SelectedItem item, bool v)
    {
        item.Active = v;

        if (ValueType == typeof(string))
        {
            CurrentValueAsString = string.Join(",", Items.Where(i => i.Active).Select(i => i.Value));
        }
        else if (ValueType.IsGenericType)
        {
            var t = ValueType.GenericTypeArguments;
            if (Activator.CreateInstance(typeof(List<>).MakeGenericType(t)) is IList instance)
            {
                foreach (var sl in Items.Where(i => i.Active))
                {
                    if (sl.Value.TryConvertTo(t[0], out var val))
                    {
                        instance.Add(val);
                    }
                }
                CurrentValue = (TValue)instance;
            }
        }

        if (OnSelectedChanged != null)
        {
            await OnSelectedChanged.Invoke(Items, Value);
        }
    }

    /// <summary>
    /// <para lang="zh">点击选择框方法</para>
    /// <para lang="en">Click checkbox method</para>
    /// </summary>
    private Task OnClick(SelectedItem item) => OnStateChanged(item, !item.Active);

    /// <summary>
    /// <para lang="zh">泛型参数约束检查</para>
    /// <para lang="en">Generic parameter constraint check</para>
    /// </summary>
    protected virtual void EnsureParameterValid()
    {
        if (ValueType.IsGenericType)
        {
            // <para lang="zh">泛型参数</para>
            // <para lang="en">Generic parameter</para>
            if (!ValueType.IsAssignableTo(typeof(IEnumerable)))
            {
                throw new NotSupportedException();
            }
        }
        else if (ValueType != typeof(string))
        {
            throw new NotSupportedException();
        }
    }

    private RenderFragment? GetChildContent(SelectedItem item) => ItemTemplate == null
        ? null
        : ItemTemplate(item);
}
