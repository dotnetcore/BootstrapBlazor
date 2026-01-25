// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ContextMenu 组件</para>
/// <para lang="en">A component that represents a context menu</para>
/// </summary>
public partial class ContextMenu
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示阴影 默认 <see langword="true" /></para>
    /// <para lang="en">Flags whether to show a shadow around the context menu. Default is <see langword="true" /></para>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 弹出前回调方法 默认 null</para>
    /// <para lang="en">Defines the callback that is executed before showing the context menu. Default is <see langword="null" /></para>
    /// </summary>
    [Parameter]
    public Func<object?, Task>? OnBeforeShowCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">The <see cref="RenderFragment"/> that represents the child content</para>
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

    private readonly List<IContextMenuItem> _contextMenuItems = [];

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
    /// <para lang="zh">弹出 ContextMenu</para>
    /// <para lang="en">Shows the <see cref="ContextMenu"/></para>
    /// </summary>
    /// <param name="args">
    ///     <para lang="zh">鼠标事件参数</para>
    ///     <para lang="en">The <see cref="MouseEventArgs"/> that invoked this event</para>
    /// </param>
    /// <param name="contextItem">
    ///     <para lang="zh">上下文项</para>
    ///     <para lang="en">Context that is associated with the clicked <see cref="ContextMenuItem"/></para>
    /// </param>
    /// <returns>
    ///     <para lang="zh">异步任务</para>
    ///     <para lang="en">An asynchronous instance of a <see cref="Task"/></para>
    /// </returns>
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
    /// <para lang="zh">增加 ContextMenuItem 方法</para>
    /// <para lang="en">Adds an <paramref name="item"/> to the menu</para>
    /// </summary>
    /// <param name="item">
    ///     <para lang="zh">要添加的项</para>
    ///     <para lang="en">The <see cref="IContextMenuItem"/> to add</para>
    /// </param>
    internal void AddItem(IContextMenuItem item) => _contextMenuItems.Add(item);

    /// <summary>
    /// <para lang="zh">移除 ContextMenuItem 方法</para>
    /// <para lang="en">Removes an <paramref name="item"/> from the menu</para>
    /// </summary>
    /// <param name="item">
    ///     <para lang="zh">要移除的项</para>
    ///     <para lang="en">The <see cref="IContextMenuItem"/> to remove</para>
    /// </param>
    internal void RemoveItem(IContextMenuItem item) => _contextMenuItems.Remove(item);
}
