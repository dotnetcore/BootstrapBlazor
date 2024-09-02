// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenu 组件
/// </summary>
public partial class ContextMenu
{
    /// <summary>
    /// 获得/设置 是否显示阴影 默认 true
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// 获得/设置 弹出前回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<object?, Task>? OnBeforeShowCallback { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    [NotNull]
    private ContextMenuZone? ContextMenuZone { get; set; }

    private string? ClassString => CssBuilder.Default("bb-cm dropdown-menu")
        .AddClass("shadow", ShowShadow)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string ZoneId => ContextMenuZone.Id;

    private List<ContextMenuItem> _contextMenuItems = [];

    private static string? GetItemClassString(bool disabled) => CssBuilder.Default("dropdown-item")
        .AddClass("disabled", disabled)
        .Build();

    private bool GetItemTriggerClick(ContextMenuItem item) => item.OnDisabledCallback?.Invoke(item, _contextItem) ?? item.Disabled;

    private static string? GetItemIconString(ContextMenuItem item) => CssBuilder.Default("cm-icon")
        .AddClass(item.Icon, !string.IsNullOrEmpty(item.Icon))
        .Build();

    private MouseEventArgs? _mouseEventArgs;

    private object? _contextItem;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ContextMenuZone.RegisterContextMenu(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_mouseEventArgs != null)
        {
            await InvokeVoidAsync("show", Id, _mouseEventArgs);
            _mouseEventArgs = null;
        }
    }

    /// <summary>
    /// 弹出 ContextMenu
    /// </summary>
    /// <param name="args"></param>
    /// <param name="contextItem"></param>
    /// <returns></returns>
    internal async Task Show(MouseEventArgs args, object? contextItem)
    {
        _contextItem = contextItem;
        _mouseEventArgs = args;
        if (OnBeforeShowCallback != null)
        {
            await OnBeforeShowCallback(contextItem);
        }
        StateHasChanged();
    }

    private async Task OnClickItem(ContextMenuItem item)
    {
        if (item.OnClick != null)
        {
            await item.OnClick(item, _contextItem);
        }
    }

    /// <summary>
    /// 增加 ContextMenuItem 方法
    /// </summary>
    /// <param name="item"></param>
    internal void AddItem(ContextMenuItem item) => _contextMenuItems.Add(item);

    /// <summary>
    /// 移除 ContextMenuItem 方法
    /// </summary>
    /// <param name="item"></param>
    internal void RemoveItem(ContextMenuItem item) => _contextMenuItems.Remove(item);
}
