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
public partial class CheckboxListGeneric<TValue> : ValidateBase<TValue>
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

    private string? GetButtonItemClassString(SelectedItem<TValue> item) => CssBuilder.Default("btn")
        .AddClass($"active bg-{Color.ToDescriptionString()}", item.Value != null && (Value?.Contains(item.Value) ?? false))
        .Build();

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem<TValue>>? Items { get; set; }

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public new List<TValue>? Value { get; set; }

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
    public Func<IEnumerable<SelectedItem<TValue>>, List<TValue>, Task>? OnSelectedChanged { get; set; }

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

    [Inject]
    [NotNull]
    private IStringLocalizerFactory? LocalizerFactory { get; set; }

    /// <summary>
    /// 获得 当前选项是否被禁用
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected bool GetDisabledState(SelectedItem<TValue> item) => IsDisabled || item.IsDisabled;

    private Func<CheckboxState, Task<bool>>? _onBeforeStateChangedCallback;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 处理 Required 标签
        if (EditContext != null && FieldIdentifier != null)
        {
            var pi = FieldIdentifier.Value.Model.GetType().GetPropertyByName(FieldIdentifier.Value.FieldName);
            if (pi != null)
            {
                var required = pi.GetCustomAttribute<RequiredAttribute>(true);
                if (required != null)
                {
                    Rules.Add(new RequiredValidator()
                    {
                        LocalizerFactory = LocalizerFactory,
                        ErrorMessage = required.ErrorMessage,
                        AllowEmptyString = required.AllowEmptyStrings
                    });
                }
            }
        }
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
                Items = innerType.ToSelectList<TValue>();
            }
            Items ??= [];
        }

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
    /// Checkbox 组件选项状态改变时触发此方法
    /// </summary>
    /// <param name="item"></param>
    /// <param name="v"></param>
    private async Task OnStateChanged(SelectedItem<TValue> item, bool v)
    {
        if (item.Value == null)
        {
            return;
        }

        Value ??= [];
        if (v)
        {
            Value.Add(item.Value);
        }
        else
        {
            Value.Remove(item.Value);
        }

        if (OnSelectedChanged != null)
        {
            await OnSelectedChanged(Items, Value);
        }
    }

    /// <summary>
    /// 点击选择框方法
    /// </summary>
    private Task OnClick(SelectedItem<TValue> item) => OnStateChanged(item, !item.Active);
}
