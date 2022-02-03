// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
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
    private List<IToolbarButton<TItem>> Buttons { get; } = new List<IToolbarButton<TItem>>();

    private readonly ConcurrentDictionary<IToolbarButton<TItem>, bool> _asyncButtonStateCache = new();

    /// <summary>
    /// Specifies the content to be rendered inside this
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 按钮点击后回调委托
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>> OnGetSelectedRows { get; set; } = () => Enumerable.Empty<TItem>();

    private async Task OnToolbarButtonClick(TableToolbarButton<TItem> button)
    {
        _asyncButtonStateCache.TryGetValue(button, out var disabled);
        if (!disabled)
        {
            _asyncButtonStateCache.TryAdd(button, true);
            if (button.OnClick != null)
            {
                await button.OnClick();
            }

            // 传递当前选中行给回调委托方法
            if (button.OnClickCallback != null)
            {
                await button.OnClickCallback(OnGetSelectedRows());
            }
            _asyncButtonStateCache.TryRemove(button, out _);
        }
    }

    private bool GetDisabled(TableToolbarButton<TItem> button)
    {
        var ret = button.IsDisabled;
        if (button.IsAsync && _asyncButtonStateCache.TryGetValue(button, out var b))
        {
            ret = b;
        }
        else if (button.IsEnableWhenSelectedOneRow)
        {
            ret = OnGetSelectedRows().Count() != 1;
        }
        return ret;
    }

    /// <summary>
    /// 添加按钮到工具栏方法
    /// </summary>
    public void AddButton(IToolbarButton<TItem> button) => Buttons.Add(button);

    /// <summary>
    /// 添加按钮到工具栏方法
    /// </summary>
    public void RemoveButton(IToolbarButton<TItem> button) => Buttons.Remove(button);
}
