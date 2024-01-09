// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class PaginationItem
{
    /// <summary>
    /// 点击页码时回调方法 参数是当前页码
    /// </summary>
    [Parameter]
    public EventCallback<int> OnClick { get; set; }

    /// <summary>
    /// 获得/设置 当前页码
    /// </summary>
    [Parameter]
    public int Index { get; set; }

    /// <summary>
    /// 获得/设置 是否激活 默认 false
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认 false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 子组件
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
