// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Cascader 组件实现类
/// </summary>
/// <typeparam name="TValue"></typeparam>
public sealed partial class Cascader<TValue>
{
    /// <summary>
    /// 当前选中节点集合
    /// </summary>
    private readonly List<CascaderItem> _selectedItems = new();


    /// <summary>
    /// 获得/设置 Cascader 内部 Input 组件 Id
    /// </summary>
    private string? InputId => string.IsNullOrEmpty(Id) ? null : $"{Id}_input";

    /// <summary>
    /// 显示文本
    /// </summary>
    private string _displayText = string.Empty;

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.None;

    /// <summary>
    /// 获得/设置 组件 PlaceHolder 文字 默认为 请选择 ...
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    public IEnumerable<CascaderItem> Items { get; set; } = Enumerable.Empty<CascaderItem>();

    /// <summary>
    /// ValueChanged 方法
    /// </summary>
    [Parameter]
    public Func<CascaderItem[], Task>? OnSelectedItemChanged { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Cascader<TValue>>? Localizer { get; set; }

    /// <summary>
    /// 获得 input 组件 Id 方法
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
    }

    private string _lastVaslue = string.Empty;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_lastVaslue == CurrentValueAsString) return;

        _lastVaslue = CurrentValueAsString;
        SetDefaultValue(CurrentValueAsString);
    }

    /// <summary>
    /// 设置默认选中
    /// </summary>
    /// <param name="defaultValue"></param>
    private void SetDefaultValue(string defaultValue)
    {
        _selectedItems.Clear();
        var item = GetNodeByValue(Items, defaultValue);
        SetSelectedNodeWithParent(item, _selectedItems);
        RefreshDisplayValue();
    }

    /// <summary>
    /// 根据指定值获取节点
    /// </summary>
    /// <param name="items"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private CascaderItem? GetNodeByValue(IEnumerable<CascaderItem> items, string value)
    {
        foreach (var item in items)
        {
            if (item.Value == value)
                return item;

            if (item.HasChildren)
            {
                var nd = GetNodeByValue(item.Items, value);
                if (nd != null)
                    return nd;
            }
        }
        return null;
    }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("dropdown")
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-control form-select")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? BackgroundColor => IsDisabled ? null : "background-color: #fff;";

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? AppendClassName => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
        .Build();

    /// <summary>
    /// 选择项是否 Active 方法
    /// </summary>
    /// <param name="className"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? ActiveItem(string className, CascaderItem item) => CssBuilder.Default(className)
        .AddClass("active", () => _selectedItems.Contains(item))
        .Build();

    /// <summary>
    /// 下拉框选项点击时调用此方法
    /// </summary>
    private async Task OnItemClick(CascaderItem item) => await SetSelectedItem(item);

    private async Task SetSelectedItem(CascaderItem item)
    {
        if (item == null) return;

        {
            _selectedItems.Clear();
            SetSelectedNodeWithParent(item, _selectedItems);
            await SetValue(item.Value);
        }

    }

    private async Task SetValue(string value)
    {
        RefreshDisplayValue();
        if (_selectedItems.Count != 1)
        {
            StateHasChanged();
        }

        CurrentValueAsString = value;

        if (OnSelectedItemChanged != null) await OnSelectedItemChanged.Invoke(_selectedItems.ToArray());
    }

    private void RefreshDisplayValue() => _displayText = string.Join("/", _selectedItems.Select(item => item.Text));

    /// <summary>
    /// 设置选中所有父节点
    /// </summary>
    /// <param name="item"></param>
    /// <param name="list"></param>
    private static void SetSelectedNodeWithParent(CascaderItem? item, List<CascaderItem> list)
    {
        if (item == null) return;

        SetSelectedNodeWithParent(item.Parent, list);
        list.Add(item);
    }
}
