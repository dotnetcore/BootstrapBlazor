// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// MainLayout 模板
/// </summary>
public partial class MainLayout : IAsyncDisposable
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
    private IStringLocalizer<MainLayout>? Localizer { get; set; }

    [NotNull]
    private string? UpdateTooltip { get; set; }

    [NotNull]
    private string? ChatTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        UpdateTooltip ??= Localizer[nameof(UpdateTooltip)];
        ChatTooltip ??= Localizer[nameof(ChatTooltip)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await DispatchService.SubscribeAsync(Dispatch);
    }

    private async Task Dispatch(DispatchEntry<MessageItem> entry)
    {
        if (entry.Entry != null)
        {
            await Toast.Show(new ToastOption()
            {
                Title = Localizer["DispatchToastTitle"],
                Content = entry.Entry.Message,
                Category = ToastCategory.Information,
                Delay = 30 * 1000,
                ForceDelay = true
            });
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DispatchService.UnSubscribeAsync(Dispatch);
        GC.SuppressFinalize(this);
    }
}
