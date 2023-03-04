// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table Toolbar 组件
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TItem))]
#endif
public partial class TableToolbar<TItem> : ComponentBase
{
    /// <summary>
    /// 获得 Toolbar 按钮集合
    /// </summary>
    private List<ButtonBase> Buttons { get; } = new();

    private readonly ConcurrentDictionary<ButtonBase, bool> _asyncButtonStateCache = new();

    /// <summary>
    /// Specifies the content to be rendered inside this
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 按钮点击后回调委托
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<IEnumerable<TItem>>? OnGetSelectedRows { get; set; }

    /// <summary>
    /// 获得/设置 是否自动收缩工具栏按钮 默认 true
    /// </summary>
    [Parameter]
    public bool IsAutoCollapsedToolbarButton { get; set; } = true;

    private string? ToolbarClassString => CssBuilder.Default("btn-toolbar btn-group")
        .AddClass("d-none d-sm-inline-flex", IsAutoCollapsedToolbarButton)
        .Build();

    private async Task OnToolbarButtonClick(TableToolbarButton<TItem> button)
    {
        _asyncButtonStateCache.TryGetValue(button, out var disabled);
        if (!disabled)
        {
            _asyncButtonStateCache.TryAdd(button, true);
            if (button.OnClick.HasDelegate)
            {
                await button.OnClick.InvokeAsync();
            }

            // 传递当前选中行给回调委托方法
            if (button.OnClickCallback != null)
            {
                await button.OnClickCallback(OnGetSelectedRows());
            }
            _asyncButtonStateCache.TryRemove(button, out _);
        }
    }

    private async Task OnToolbarConfirmButtonClick(TableToolbarPopconfirmButton<TItem> button)
    {
        _asyncButtonStateCache.TryGetValue(button, out var disabled);
        if (!disabled)
        {
            _asyncButtonStateCache.TryAdd(button, true);
            if (button.OnClick.HasDelegate)
            {
                await button.OnClick.InvokeAsync();
            }

            if (button.OnConfirm != null)
            {
                await button.OnConfirm();
            }

            // 传递当前选中行给回调委托方法
            if (button.OnConfirmCallback != null)
            {
                await button.OnConfirmCallback(OnGetSelectedRows());
            }
            _asyncButtonStateCache.TryRemove(button, out _);
        }
    }

    private bool GetDisabled(ButtonBase button)
    {
        var ret = button.IsDisabled;
        if (button.IsAsync && _asyncButtonStateCache.TryGetValue(button, out var b))
        {
            ret = b;
        }
        else if (button is TableToolbarButton<TItem> { IsEnableWhenSelectedOneRow: true } || button is TableToolbarPopconfirmButton<TItem> { IsEnableWhenSelectedOneRow: true })
        {
            ret = OnGetSelectedRows().Count() != 1;
        }
        return ret;
    }

    /// <summary>
    /// 添加按钮到工具栏方法
    /// </summary>
    public void AddButton(ButtonBase button) => Buttons.Add(button);

    /// <summary>
    /// 移除按钮到工具栏方法
    /// </summary>
    public void RemoveButton(ButtonBase button) => Buttons.Remove(button);
}
