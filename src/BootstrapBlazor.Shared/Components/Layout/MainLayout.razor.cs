// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Layout;

/// <summary>
/// Main layout dispatch subscriptions.
/// </summary>
public partial class MainLayout : IAsyncDisposable
{
    [Inject]
    [NotNull]
    private IDispatchService<MessageItem>? DispatchService { get; set; }

    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await DispatchService.SubscribeAsync(Dispatch);
    }

    private Task Dispatch(DispatchEntry<MessageItem> entry) => InvokeAsync(async () =>
    {
        if (entry.Entry != null)
        {
            await Toast.Show(new ToastOption
            {
                Title = "Dispatch 服务测试",
                Content = System.Net.WebUtility.HtmlEncode(entry.Entry.Message),
                Category = ToastCategory.Information,
                Delay = 30 * 1000,
                ForceDelay = true
            });
        }
    });

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DispatchService.UnSubscribeAsync(Dispatch);
        GC.SuppressFinalize(this);
    }
}
