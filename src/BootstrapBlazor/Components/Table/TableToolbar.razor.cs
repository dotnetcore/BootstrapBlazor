// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table Toolbar 组件</para>
/// <para lang="en">Table Toolbar Component</para>
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TItem))]
#endif
public partial class TableToolbar<TItem> : ComponentBase
{
    private readonly List<IToolbarComponent> _buttons = [];

    private readonly ConcurrentDictionary<ButtonBase, bool> _asyncButtonStateCache = new(ReferenceEqualityComparer.Instance);

    /// <summary>
    /// <para lang="zh">获得/设置 子组件内容</para>
    /// <para lang="en">Gets or sets the content to be rendered inside this</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮点击后回调委托</para>
    /// <para lang="en">Gets or sets button click callback delegate</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<IEnumerable<TItem>>? OnGetSelectedRows { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动收缩工具栏按钮，默认 true</para>
    /// <para lang="en">Gets or sets whether to auto collapse toolbar buttons. Default is true</para>
    /// </summary>
    [Parameter]
    public bool IsAutoCollapsedToolbarButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 工具栏按钮收缩后是否继承原先按钮的颜色样式，默认 false</para>
    /// <para lang="en">Gets or sets whether to inherit button color style when toolbar buttons collapsed. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowColorWhenToolbarButtonsCollapsed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 移动端按钮图标</para>
    /// <para lang="en">Gets or sets mobile button icon</para>
    /// </summary>
    [Parameter]
    public string? GearIcon { get; set; }

    private string? ToolbarClassString => CssBuilder.Default("btn-toolbar btn-group")
        .AddClass("d-none d-sm-inline-flex", IsAutoCollapsedToolbarButton)
        .Build();

    private string? GetItemClass(ButtonBase button) => CssBuilder.Default("dropdown-item")
        .AddClass("disabled", GetDisabled(button))
        .AddClass($"dropdown-item-btn-{button.Color.ToDescriptionString()}",
            ShowColorWhenToolbarButtonsCollapsed &&
            !button.IsOutline &&
            button.Color != Color.None &&
            button.Color != Color.Link)
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
    /// <para lang="zh">添加按钮到工具栏方法</para>
    /// <para lang="en">Add button to toolbar method</para>
    /// </summary>
    public void AddButton(IToolbarComponent button) => _buttons.Add(button);

    /// <summary>
    /// <para lang="zh">移除按钮从工具栏方法</para>
    /// <para lang="en">Remove button from toolbar method</para>
    /// </summary>
    public void RemoveButton(IToolbarComponent button) => _buttons.Remove(button);
}
