// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
            if (Options.MessageDelay != 0)
            {
                option.Delay = Options.MessageDelay;
            }
        }
        await Invoke(option, message);
    }
}
