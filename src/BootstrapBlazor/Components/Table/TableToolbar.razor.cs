// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    private readonly List<IToolbarComponent> _buttons = [];

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

    /// <summary>
    /// 获得/设置 移动端按钮图标
    /// </summary>
    [Parameter]
    public string? GearIcon { get; set; }

    private string? ToolbarClassString => CssBuilder.Default("btn-toolbar btn-group")
        .AddClass("d-none d-sm-inline-flex", IsAutoCollapsedToolbarButton)
        .Build();

    private string? GetItemClass(ButtonBase button) => CssBuilder.Default("dropdown-item")
        .AddClass("disabled", GetDisabled(button))
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

            if (button.OnClickWithoutRender != null)
            {
                await button.OnClickWithoutRender();
            }

            // 传递当前选中行给回调委托方法
            if (button.OnClickCallback != null)
            {
                await button.OnClickCallback(OnGetSelectedRows());
            }
            _asyncButtonStateCache.TryRemove(button, out _);
        }
    }

    private async Task OnConfirm(TableToolbarPopConfirmButton<TItem> button)
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
        else if (button is ITableToolbarButton<TItem> tb)
        {
            ret |= tb.IsDisabledCallback == null ? (tb.IsEnableWhenSelectedOneRow && OnGetSelectedRows().Count() != 1) : tb.IsDisabledCallback(OnGetSelectedRows());
        }
        return ret;
    }

    /// <summary>
    /// 添加按钮到工具栏方法
    /// </summary>
    public void AddButton(IToolbarComponent button) => _buttons.Add(button);

    /// <summary>
    /// 移除按钮到工具栏方法
    /// </summary>
    public void RemoveButton(IToolbarComponent button) => _buttons.Remove(button);
}
