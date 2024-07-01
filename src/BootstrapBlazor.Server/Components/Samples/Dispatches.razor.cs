// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Dispatches 组件
/// </summary>
public partial class Dispatches
{
    [Inject]
    [NotNull]
    private WebClientService? ClientService { get; set; }

    [Inject]
    [NotNull]
    private IIpLocatorFactory? IpLocatorFactory { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    private async Task OnDispatch()
    {
        var message = $"{DateTime.Now:HH:mm:ss} {Localizer["DispatchNoticeMessage"]}";
        var item = new DispatchEntry<MessageItem>()
        {
            Name = nameof(MessageItem)
        };

        // 获得当前用户 IP 地址
        var clientInfo = await ClientService.GetClientInfo();
        if (clientInfo.Ip != null)
        {
            var provider = IpLocatorFactory.Create(Options.CurrentValue.IpLocatorOptions.ProviderName);
            var location = await provider.Locate(clientInfo.Ip);
            message = $"{message} {location}";
        }

        item.Entry = new MessageItem() { Message = message };
        DispatchService.Dispatch(item);
        await Task.Delay(30 * 1000);
    }
}
