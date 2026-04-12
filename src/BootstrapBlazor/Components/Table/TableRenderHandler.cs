// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

sealed class TableRenderHandler : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件内容</para>
    /// <para lang="en">Gets or sets the content to be rendered inside this</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载列前回调方法</para>
    /// <para lang="en">Gets or sets the callback delegate for loading columns</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnLoadTableColumns { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 首次渲染回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate for the first load</para>
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnRenderAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (OnLoadTableColumns != null)
        {
            await OnLoadTableColumns();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (OnRenderAsync != null)
        {
            await OnRenderAsync(firstRender);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent);
    }
}
