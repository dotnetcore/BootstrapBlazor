// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Collections;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// CheckboxList 组件基类
/// </summary>
public partial class CheckboxList<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("checkbox-list form-control")
        .AddClass("no-border", !ShowBorder && ValidCss != "is-invalid")
        .AddClass("is-vertical", IsVertical)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 组件内部 Checkbox 项目样式
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
        .AddClass($"active bg-{Color.ToDescriptionString()}", CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries).Contains(item.Value))
        .Build();

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否为按钮样式 默认 false
    /// </summary>
    [Parameter]
    public bool IsButton { get; set; }

    /// <summary>
    /// 获得/设置 Checkbox 组件布局样式
    /// </summary>
    [Parameter]
    public string? CheckboxItemClass { get; set; }

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
    /// 获得/设置 SelectedItemChanged 方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem>, TValue, Task>? OnSelectedChanged { get; set; }

    /// <summary>
    /// 获得/设置 最多选中数量
    /// </summary>
    [Parameter]
    public int MaxSelectedCount { get; set; }

    /// <summary>
    /// 获得/设置 超过最大选中数量时回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnMaxSelectedCountExceed { get; set; }

    /// <summary>
    /// 获得/设置 项模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得 当前选项是否被禁用
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected bool GetDisabledState(SelectedItem item) => IsDisabled || item.IsDisabled;

    private Func<CheckboxState, Task<bool>>? _onBeforeStateChangedCallback;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        EnsureParameterValid();

        // 处理 Required 标签
        AddRequiredValidator();
    }

    /// <summary>
    /// OnParametersSet 方法
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
    /// <inheritdoc/>
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
        // 通过 Value 对集合进行赋值
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
    /// 
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
    /// Checkbox 组件选项状态改变时触发此方法
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
    /// 点击选择框方法
    /// </summary>
    private Task OnClick(SelectedItem item) => OnStateChanged(item, !item.Active);

    /// <summary>
    /// 泛型参数约束检查
    /// </summary>
    protected virtual void EnsureParameterValid()
    {
        if (ValueType.IsGenericType)
        {
            // 泛型参数
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
