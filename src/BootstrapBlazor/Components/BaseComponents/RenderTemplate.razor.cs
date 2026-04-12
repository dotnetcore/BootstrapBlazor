// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">RenderTemplate 组件</para>
/// <para lang="en">RenderTemplate component</para>
/// </summary>
public partial class RenderTemplate : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件内容</para>
    /// <para lang="en">Gets or sets the child component</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 首次渲染回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate for the first load</para>
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnRenderAsync { get; set; }

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
    /// <para lang="zh">触发组件重新渲染</para>
    /// <para lang="en">Render method</para>
    /// </summary>
    public void Render()
    {
        StateHasChanged();
    }
}
