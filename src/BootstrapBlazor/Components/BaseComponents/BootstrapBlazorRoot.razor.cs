// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorRoot 组件
/// </summary>
public partial class BootstrapBlazorRoot
{
    [Inject]
    [NotNull]
    private ICacheManager? Cache { get; set; }

    /// <summary>
    /// 获得/设置 自组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得 Message 组件实例
    /// </summary>
    [NotNull]
    public Message? MessageContainer { get; private set; }

    /// <summary>
    /// 获得 Toast 组件实例
    /// </summary>
    [NotNull]
    public Toast? ToastContainer { get; private set; }

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Cache.SetStartTime();

        await base.SetParametersAsync(parameters);
    }
}
