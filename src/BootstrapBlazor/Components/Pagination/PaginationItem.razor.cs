// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">PaginationItem 组件用于分页组件中的单个页码项</para>
/// <para lang="en">PaginationItem component for individual page item in pagination</para>
/// </summary>
public partial class PaginationItem
{
    /// <summary>
    /// <para lang="zh">点击页码时回调方法，参数是当前页码</para>
    /// <para lang="en">Callback method when page link is clicked. Parameter is current page index</para>
    /// </summary>
    [Parameter]
    public EventCallback<int> OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前页码</para>
    /// <para lang="en">Gets or sets the current page index</para>
    /// </summary>
    [Parameter]
    public int Index { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否激活，默认为 false</para>
    /// <para lang="en">Gets or sets whether active. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用，默认为 false</para>
    /// <para lang="en">Gets or sets whether disabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板</para>
    /// <para lang="en">Gets or sets the child component template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("page-item")
        .AddClass("active", IsActive)
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnClickItem()
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(Index);
        }
    }
}
