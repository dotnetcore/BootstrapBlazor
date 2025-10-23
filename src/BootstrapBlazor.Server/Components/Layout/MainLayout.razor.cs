// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// MainLayout 模板
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
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

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
            await Toast.Show(new ToastOption()
            {
                Title = "Dispatch 服务测试",
                Content = entry.Entry.Message,
                Category = ToastCategory.Information,
                Delay = 30 * 1000,
                ForceDelay = true
            });
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
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
