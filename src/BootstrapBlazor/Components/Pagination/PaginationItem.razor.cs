// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"></para>
/// <para lang="en"></para>
/// </summary>
public partial class PaginationItem
{
    /// <summary>
    /// <para lang="zh">点击页码时回调方法 参数是当前页码</para>
    /// <para lang="en">Callback method when page link is clicked. Parameter is current page index</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<int> OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前页码</para>
    /// <para lang="en">Gets or sets Current Page Index</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Index { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否激活 默认 false</para>
    /// <para lang="en">Gets or sets Whether active. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认 false</para>
    /// <para lang="en">Gets or sets Whether disabled. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">子组件</para>
    /// <para lang="en">Child Content</para>
    /// <para><version>10.2.2</version></para>
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
