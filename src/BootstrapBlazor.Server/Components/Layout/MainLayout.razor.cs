// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// 
/// </summary>
public partial class MainLayout : IDisposable
{
    [Inject]
    [NotNull]
    private IDispatchService<MessageItem>? DispatchService { get; set; }

    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    [Inject]
    [NotNull]
    private WebClientService? ClientService { get; set; }

    [Inject]
    [NotNull]
    private IIpLocatorFactory? IpLocatorFactory { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? WebsiteOption { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<BaseLayout>? Localizer { get; set; }

    [NotNull]
    private string? Title { get; set; }

    [NotNull]
    private string? ChatTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DispatchService.Subscribe(Dispatch);

        Title ??= Localizer[nameof(Title)];
        ChatTooltip ??= Localizer[nameof(ChatTooltip)];
    }

    private async Task Dispatch(DispatchEntry<MessageItem> entry)
    {
        if (entry.Entry != null)
        {
            // 获得当前用户 IP 地址
            var clientInfo = await ClientService.GetClientInfo();
            if (clientInfo.Ip != null)
            {
                var provider = IpLocatorFactory.Create();
                var location = await provider.Locate(clientInfo.Ip);
                await Toast.Show(new ToastOption()
                {
                    Title = "Dispatch 服务测试",
                    Content = $"{entry.Entry.Message} 来自 {location}",
                    Category = ToastCategory.Information,
                    Delay = 30 * 1000,
                    ForceDelay = true
                });
            }
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            DispatchService.UnSubscribe(Dispatch);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
