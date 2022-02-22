// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Radio<TValue>
{
    /// <summary>
    /// 获得/设置 点击回调方法
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnClick { get; set; }

    /// <summary>
    /// 获得/设置 Radio 组名称一般来讲需要设置 默认为 null 未设置
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? GroupName { get; set; }

    private void OnClickHandler()
    {
        OnClick?.Invoke(Value);
    }
}
