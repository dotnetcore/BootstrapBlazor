// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ContextMenuZone 组件</para>
///  <para lang="en">ContextMenuZone component</para>
/// </summary>
public partial class ContextMenuZone
{
    /// <summary>
    ///  <para lang="zh">获得/设置 子组件</para>
    ///  <para lang="en">Get/Set child content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private ContextMenu? _contextMenu;

    private string? ClassString => CssBuilder.Default("bb-cm-zone")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh">Trigger 调用</para>
    ///  <para lang="en">Trigger call</para>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="contextItem"></param>
    /// <returns></returns>
    internal async Task OnContextMenu(MouseEventArgs args, object? contextItem)
    {
        // 弹出关联菜单
        if (_contextMenu != null)
        {
            await _contextMenu.Show(args, contextItem);
        }
    }

    /// <summary>
    ///  <para lang="zh">ContextMenu 组件调用</para>
    ///  <para lang="en">ContextMenu component call</para>
    /// </summary>
    /// <param name="contextMenu"></param>
    internal void RegisterContextMenu(ContextMenu contextMenu) => _contextMenu = contextMenu;
}
