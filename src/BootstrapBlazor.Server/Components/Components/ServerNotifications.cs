// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// Server-side notifications shared by the existing demo pages.
/// </summary>
public class ServerNotifications : ComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<GiteePostBody>? CommitDispatchService { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<bool>? RebootDispatchService { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<MessageItem>? MessageDispatchService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CommitDispatchService.Subscribe(NotifyCommit);
        RebootDispatchService.Subscribe(NotifyReboot);
        MessageDispatchService.Subscribe(NotifyMessage);
    }

    private async Task NotifyMessage(DispatchEntry<MessageItem> entry)
    {
        if (entry.Entry != null)
        {
            await Toast.Show(new ToastOption
            {
                Title = "Dispatch 服务测试",
                Content = entry.Entry.Message,
                Category = ToastCategory.Information,
                Delay = 30 * 1000,
                ForceDelay = true
            });
        }
    }

    private async Task NotifyCommit(DispatchEntry<GiteePostBody> payload)
    {
        if (payload.CanDispatch())
        {
            var option = new ToastOption()
            {
                Category = ToastCategory.Information,
                Title = "代码提交推送通知",
                Delay = 120 * 1000,
                ForceDelay = true,
                ChildContent = BootstrapDynamicComponent.CreateComponent<CommitItem>(new Dictionary<string, object?>
                {
                    [nameof(CommitItem.Item)] = payload.Entry
                }).Render()
            };
            await Toast.Show(option);
        }
    }

    private ToastOption? _option;

    private async Task NotifyReboot(DispatchEntry<bool> payload)
    {
        if (payload.Entry)
        {
            _option = new ToastOption()
            {
                Category = ToastCategory.Information,
                Title = "网站更新中 ...",
                IsAutoHide = false,
                ChildContent = BootstrapDynamicComponent.CreateComponent<RebootCountDown>().Render(),
                PreventDuplicates = true
            };
            await Toast.Show(_option);
        }
        else if (_option != null)
        {
            await InvokeAsync(_option.Close);
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            CommitDispatchService.UnSubscribe(NotifyCommit);
            RebootDispatchService.UnSubscribe(NotifyReboot);
            MessageDispatchService.UnSubscribe(NotifyMessage);
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
