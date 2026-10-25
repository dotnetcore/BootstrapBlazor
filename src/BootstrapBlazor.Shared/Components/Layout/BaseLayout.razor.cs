// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Layout;

/// <summary>
/// Base layout dispatch subscriptions.
/// </summary>
public partial class BaseLayout : IAsyncDisposable
{
    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<CommitNotification>? CommitDispatchService { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<bool>? RebootDispatchService { get; set; }

    private ToastOption? _updateToast;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await CommitDispatchService.SubscribeAsync(NotifyCommit);
        await RebootDispatchService.SubscribeAsync(NotifyReboot);
    }

    private Task NotifyCommit(DispatchEntry<CommitNotification> entry) => InvokeAsync(async () =>
    {
        if (entry.Entry != null)
        {
            await Toast.Show(new ToastOption
            {
                Category = ToastCategory.Information,
                Title = "代码提交推送通知",
                Delay = 120 * 1000,
                ForceDelay = true,
                ChildContent = BootstrapDynamicComponent.CreateComponent<CommitItem>(new Dictionary<string, object?>
                {
                    [nameof(CommitItem.Item)] = entry.Entry
                }).Render()
            });
        }
    });

    private Task NotifyReboot(DispatchEntry<bool> entry) => InvokeAsync(async () =>
    {
        if (entry.Entry && _updateToast == null)
        {
            _updateToast = new ToastOption
            {
                Category = ToastCategory.Information,
                Title = "网站更新中 ...",
                IsAutoHide = false,
                ChildContent = BootstrapDynamicComponent.CreateComponent<RebootCountDown>().Render(),
                PreventDuplicates = true
            };
            await Toast.Show(_updateToast);
        }
        else if (!entry.Entry && _updateToast != null)
        {
            await _updateToast.Close();
            _updateToast = null;
        }
    });

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await CommitDispatchService.UnSubscribeAsync(NotifyCommit);
        await RebootDispatchService.UnSubscribeAsync(NotifyReboot);
        GC.SuppressFinalize(this);
    }
}
