// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        if (!option.ForceDelay && options.CurrentValue.SwalDelay > 0)
        {
            option.Delay = options.CurrentValue.SwalDelay;
        }

        await Invoke(option, swal);
    }
}
