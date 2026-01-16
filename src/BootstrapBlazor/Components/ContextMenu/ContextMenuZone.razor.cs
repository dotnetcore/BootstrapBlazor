// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ContextMenuZone 组件</para>
/// <para lang="en">ContextMenuZone component</para>
/// </summary>
public partial class ContextMenuZone
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets the child content.</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private ContextMenu? _contextMenu;

    private string? ClassString => CssBuilder.Default("bb-cm-zone")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">Trigger 调用</para>
    /// <para lang="en">Trigger call</para>
    /// </summary>
    /// <param name="args"><para lang="zh">鼠标事件参数</para><para lang="en">Mouse event arguments</para></param>
    /// <param name="contextItem"><para lang="zh">上下文项</para><para lang="en">Context item</para></param>
    /// <returns><para lang="zh">异步任务</para><para lang="en">An asynchronous task</para></returns>
    internal async Task OnContextMenu(MouseEventArgs args, object? contextItem)
    {
        // 弹出关联菜单
        if (_contextMenu != null)
        {
            await _contextMenu.Show(args, contextItem);
        }
    }

    /// <summary>
    /// <para lang="zh">ContextMenu 组件调用</para>
    /// <para lang="en">Called by the ContextMenu component</para>
    /// </summary>
    /// <param name="contextMenu"><para lang="zh">要注册的 ContextMenu 组件</para><para lang="en">The ContextMenu component to register</para></param>
    internal void RegisterContextMenu(ContextMenu contextMenu) => _contextMenu = contextMenu;
}
