// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class SelectBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 设置搜索图标 默认 fa-solid fa-magnifying-glass
    /// </summary>
    [Parameter]
    public string SearchIcon { get; set; } = "fa-solid fa-magnifying-glass";

    /// <summary>
    /// 获得/设置 字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// 获得/设置 选项模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 弹窗位置 默认为 Bottom
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// 获得/设置 是否使用 Popover 渲染下拉框 默认 false
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected string? ToggleString => IsPopover ? null : "dropdown";

    /// <summary>
    /// 
    /// </summary>
    protected string? DropdownMenuClassString => IsPopover ? "dropdown-menu" : "dropdown-menu shadow";

    /// <summary>
    /// 
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    /// 
    /// </summary>
    protected string SearchText { get; set; } = "";
}
