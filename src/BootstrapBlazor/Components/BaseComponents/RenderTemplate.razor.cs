// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">RenderTemplate component</para>
///  <para lang="en">RenderTemplate component</para>
/// </summary>
public partial class RenderTemplate
{
    /// <summary>
    ///  <para lang="zh">获得/设置 the child component</para>
    ///  <para lang="en">Gets or sets the child component</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the 回调 委托 for the first load</para>
    ///  <para lang="en">Gets or sets the callback delegate for the first load</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnRenderAsync { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (OnRenderAsync != null)
        {
            await OnRenderAsync(firstRender);
        }
    }

    /// <summary>
    ///  <para lang="zh">Render method</para>
    ///  <para lang="en">Render method</para>
    /// </summary>
    public void Render()
    {
        StateHasChanged();
    }
}
