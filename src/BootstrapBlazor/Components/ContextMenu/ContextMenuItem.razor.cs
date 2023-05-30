// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuItem 类
/// </summary>
public partial class ContextMenuItem
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
    /// 获得/设置 是否被禁用 默认 false
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// 获得/设置 点击回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<ContextMenuItem, object?, Task>? OnClick { get; set; }

    [CascadingParameter]
    private ContextMenu? ContextMenu { get; set; }

    private string? ClassString => CssBuilder.Default("dropdown-item")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? IconString => CssBuilder.Default("cm-icon")
        .AddClass(Icon, !string.IsNullOrEmpty(Icon))
        .Build();

    private async Task OnClickItem()
    {
        if (!Disabled && OnClick != null)
        {
            await OnClick(this, ContextMenu?.GetContextItem());
        }
    }
}
