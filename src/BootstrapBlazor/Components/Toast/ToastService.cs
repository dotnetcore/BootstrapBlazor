// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toast 弹出窗服务类</para>
/// <para lang="en">Toast Popup Window Service Class</para>
/// </summary>
/// <param name="options"></param>
public class ToastService(IOptionsMonitor<BootstrapBlazorOptions> options) : BootstrapServiceBase<ToastOption>
{
    /// <summary>
    /// <para lang="zh">显示 Toast 弹窗方法</para>
    /// <para lang="en">Shows the Toast popup window</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="toastContainer"><para lang="zh">指定弹窗组件，默认为 null 使用 BootstrapBlazorRoot 组件内置弹窗组件</para><para lang="en">Specifies the popup component. Default is null (uses the built-in popup component in BootstrapBlazorRoot)</para></param>
    public async Task Show(ToastOption option, ToastContainer? toastContainer = null)
    {
        if (!option.ForceDelay && options.CurrentValue.ToastDelay != 0)
        {
            option.Delay = options.CurrentValue.ToastDelay;
        }
        await Invoke(option, toastContainer);
    }
}
