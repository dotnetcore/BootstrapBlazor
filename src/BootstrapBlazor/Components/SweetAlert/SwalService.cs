// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SweetAlert 弹窗服务
/// </summary>
public class SwalService(IOptionsMonitor<BootstrapBlazorOptions> options) : BootstrapServiceBase<SwalOption>
{
    /// <summary>
    /// Show 方法
    /// </summary>
    /// <param name="option"><see cref="SwalOption"/> 实例</param>
    /// <param name="swal">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public async Task Show(SwalOption option, SweetAlert? swal = null)
    {
        if (!option.ForceDelay && options.CurrentValue.SwalDelay != 0)
        {
            option.Delay = options.CurrentValue.SwalDelay;
        }

        await Invoke(option, swal);
    }
}
