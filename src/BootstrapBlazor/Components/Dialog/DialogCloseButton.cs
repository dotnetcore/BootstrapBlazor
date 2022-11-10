// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗内关闭按钮组件
/// </summary>
public partial class DialogCloseButton : Button
{
    [CascadingParameter]
    private Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (OnCloseAsync != null)
        {
            OnClickWithoutRender = OnCloseAsync;
        }
    }
}
