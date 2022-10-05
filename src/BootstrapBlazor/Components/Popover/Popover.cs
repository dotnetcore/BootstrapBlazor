// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Popover 弹出窗组件
/// </summary>
public class Popover : Tooltip
{
    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ToggleString = "popover";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async ValueTask JSInvokeAsync()
    {
        if (!string.IsNullOrEmpty(Content))
        {
            await JSRuntime.InvokeVoidByIdAsync(identifier: "bb.Popover.init", Id, Title, Content);
        }
    }
}
