// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// Stack Item 组件
/// </summary>
public class StackItem : BootstrapComponentBase, IDisposable
{
    [CascadingParameter]
    private Stack? Stack { get; set; }

    /// <summary>
    /// 获得/设置 内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否自动充满 默认 false
    /// </summary>
    [Parameter]
    public bool IsFill { get; set; }

    /// <summary>
    /// 获得/设置 垂直布局模式 默认 StackAlignItems.Stretch
    /// </summary>
    [Parameter]
    public StackAlignItems AlignSelf { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Stack?.AddItem(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Stack == null)
        {
            builder.AddContent(0, ChildContent);
        }
    }

    /// <summary>
    /// 销毁资源
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Stack?.RemoveItem(this);
        }
    }

    /// <summary>
    /// 销毁资源
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
