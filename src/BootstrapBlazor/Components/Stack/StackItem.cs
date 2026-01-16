// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Stack Item 组件</para>
/// <para lang="en">Stack Item Component</para>
/// </summary>
public class StackItem : BootstrapComponentBase, IDisposable
{
    [CascadingParameter]
    private Stack? Stack { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容</para>
    /// <para lang="en">Get/Set Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动充满 默认 false</para>
    /// <para lang="en">Get/Set Is Fill. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsFill { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 垂直布局模式 默认 StackAlignItems.Stretch</para>
    /// <para lang="en">Get/Set Align Self. Default StackAlignItems.Stretch</para>
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
    /// <para lang="zh">销毁资源</para>
    /// <para lang="en">Dispose</para>
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
    /// <para lang="zh">销毁资源</para>
    /// <para lang="en">Dispose</para>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
