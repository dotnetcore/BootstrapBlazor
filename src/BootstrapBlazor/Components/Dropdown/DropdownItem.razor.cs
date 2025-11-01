// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// DropdownItem 组件
/// </summary>
public partial class DropdownItem
{
    /// <summary>
    /// 获得/设置 显示文本
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否被禁用 默认 false 优先级低于 <see cref="OnDisabledCallback"/>
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// 获得/设置 是否被禁用回调方法 默认 null 优先级高于 <see cref="Disabled"/>
    /// </summary>
    [Parameter]
    public Func<bool>? OnDisabledCallback { get; set; }

    /// <summary>
    /// 获得/设置 点击回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ItemIconString => CssBuilder.Default("dropdown-item-icon")
        .AddClass(Icon, !string.IsNullOrEmpty(Icon))
        .Build();

    private string? ItemClassString => CssBuilder.Default("dropdown-item")
        .AddClass("disabled", CanTriggerClick)
        .Build();

    private bool CanTriggerClick => OnDisabledCallback?.Invoke() ?? Disabled;

    private async Task OnClickItem()
    {
        if (OnClick != null)
        {
            await OnClick();
        }
    }
}
