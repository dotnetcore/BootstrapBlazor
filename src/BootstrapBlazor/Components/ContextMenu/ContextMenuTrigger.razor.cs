// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuTrigger 组件
/// </summary>
public partial class ContextMenuTrigger : IDisposable
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 包裹组件 TagName 默认为 div
    /// </summary>
    [Parameter]
    public string WrapperTag { get; set; } = "div";

    /// <summary>
    /// 获得/设置 是否停止广播 默认 true
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; } = true;

    /// <summary>
    /// 获得/设置 上下文数据
    /// </summary>
    [Parameter]
    public object? ContextItem { get; set; }

    [CascadingParameter]
    [NotNull]
    private ContextMenuZone? ContextMenuZone { get; set; }

    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ContextMenuZone.Triggers.Add(this);
    }

    private Task OnClickContextMenu() => ContextMenuZone.OnContextMenu(ContextItem);

    /// <summary>
    /// 释放资源方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ContextMenuZone.Triggers.Remove(this);
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
