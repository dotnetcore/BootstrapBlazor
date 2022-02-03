// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Dropdown<TValue>
{
    /// <summary>
    /// 获得 按钮弹出方向集合
    /// </summary>
    /// <returns></returns>
    private string? DirectionClassName => CssBuilder.Default()
        .AddClass($"btn-group", DropdownType == DropdownType.ButtonGroup)
        .AddClass($"{Direction.ToDescriptionString()}", DropdownType == DropdownType.DropdownMenu)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ButtonClassName => CssBuilder.Default("btn")
        .AddClass("dropdown-toggle", !ShowSplit)
        .AddClass($"btn-primary", Color == Color.None)
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .Build();

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("btn dropdown-toggle")
      .AddClass("dropdown-toggle-split")
      .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
      .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
      .Build();

    /// <summary>
    /// 获得 是否分裂式按钮
    /// </summary>
    private string? DropdownToggle => !ShowSplit ? "dropdown" : null;

    /// <summary>
    /// 菜单对齐方式样式
    /// </summary>
    private string? MenuAlignmentClass => CssBuilder.Default("dropdown-menu shadow")
        .AddClass($"dropdown-menu-{MenuAlignment.ToDescriptionString()}", MenuAlignment == Alignment.Right)
        .Build();

    /// <summary>
    /// 获得/设置 设置当前项是否 Active 方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", () => item.Value == CurrentValueAsString)
        .Build();

    /// <summary>
    /// 获得/设置 是否开启分裂式 默认 false
    /// </summary>
    [Parameter]
    public bool ShowSplit { get; set; }

    /// <summary>
    /// 获得/设置 获取菜单对齐方式 默认 none 未设置
    /// </summary>
    [Parameter]
    public Alignment MenuAlignment { get; set; }

    /// <summary>
    /// 获得/设置 下拉选项方向 默认 Dropdown 向下
    /// </summary>
    [Parameter]
    public Direction Direction { get; set; }

    /// <summary>
    /// 获得/设置 组件尺寸 默认 none 未设置
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 是否固定按钮文字 更改下拉框选项时按钮文字保持不变 默认 false 不固定
    /// </summary>
    [Parameter]
    public bool IsFixedButtonText { get; set; }

    /// <summary>
    /// 获得/设置 下拉菜单中是否显示固定文字 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFixedButtonTextInDropdown { get; set; }

    /// <summary>
    /// 获得/设置 下拉框渲染类型 默认 DropdownMenu 下拉菜单
    /// </summary>
    [Parameter]
    public DropdownType DropdownType { get; set; }

    [NotNull]
    private List<SelectedItem>? DataSource { get; set; }

    private string? ButtonText { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 合并 Items 与 Options 集合
        Items ??= Enumerable.Empty<SelectedItem>();

        if (!Items.Any() && typeof(TValue).IsEnum())
        {
            Items = typeof(TValue).ToSelectList();
        }

        DataSource = Items.ToList();

        SelectedItem = DataSource.FirstOrDefault(i => i.Value.Equals(CurrentValueAsString, StringComparison.OrdinalIgnoreCase))
            ?? DataSource.FirstOrDefault(i => i.Active)
            ?? DataSource.FirstOrDefault();

        ButtonText ??= SelectedItem?.Text;
    }

    private IEnumerable<SelectedItem> GetItems() => (IsFixedButtonText && !ShowFixedButtonTextInDropdown)
        ? Items.Where(i => i.Text != ButtonText)
        : Items;

    /// <summary>
    /// 下拉框选项点击时调用此方法
    /// </summary>
    protected async Task OnItemClick(SelectedItem item)
    {
        if (!IsDisabled && !item.IsDisabled)
        {
            item.Active = true;
            SelectedItem = item;
            CurrentValueAsString = item.Value;

            // 触发 SelectedItemChanged 事件
            if (OnSelectedItemChanged != null)
            {
                await OnSelectedItemChanged.Invoke(SelectedItem);
            }
        }
    }

    private string? GetText => IsFixedButtonText ? ButtonText : SelectedItem?.Text;
}
