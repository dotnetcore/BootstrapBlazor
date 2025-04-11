// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// MessageService 消息弹窗服务
/// </summary>
/// <param name="option"></param>
public class MessageService(IOptionsMonitor<BootstrapBlazorOptions> option) : BootstrapServiceBase<MessageOption>
{
    private BootstrapBlazorOptions Options { get; } = option.CurrentValue;

    /// <summary>
    /// Show 方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="message">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public async Task Show(MessageOption option, Message? message = null)
    {
        if (!option.ForceDelay)
        {
            if (Options.MessageDelay > 0)
            {
                option.Delay = Options.MessageDelay;
            }
        }
        await Invoke(option, message);
    }
}
