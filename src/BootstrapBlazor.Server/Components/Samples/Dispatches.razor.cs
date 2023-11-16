// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Dispatches 组件
/// </summary>
public partial class Dispatches
{
    private async Task OnDispatch()
    {
        DispatchService.Dispatch(new DispatchEntry<MessageItem>() { Name = nameof(MessageItem), Entry = new MessageItem() { Message = $"{DateTime.Now:HH:mm:ss} {Localizer["DispatchNoticeMessage"]}" } });
        await Task.Delay(30 * 1000);
    }
}
