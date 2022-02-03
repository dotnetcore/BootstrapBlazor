// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 消息分发示例
/// </summary>
public partial class Dispatchs
{
    [Inject]
    [NotNull]
    private IDispatchService<MessageItem>? DispatchService { get; set; }

    private async Task OnDispatch()
    {
        DispatchService.Dispatch(new DispatchEntry<MessageItem>()
        {
            Name = nameof(MessageItem),
            Entry = new MessageItem()
            {
                Message = $"{DateTime.Now:HH:mm:ss} 测试通知消息"
            }
        });
        await Task.Delay(30 * 1000);
    }
}
