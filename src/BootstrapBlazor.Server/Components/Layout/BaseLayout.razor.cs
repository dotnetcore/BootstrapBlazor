// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// 母版页基类
/// </summary>
public partial class BaseLayout : IDisposable
{
    [Inject]
    [NotNull]
    private IStringLocalizer<BaseLayout>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<GiteePostBody>? CommitDispatchService { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<bool>? RebootDispatchService { get; set; }

    [NotNull]
    private string? FlowText { get; set; }

    [NotNull]
    private string? InstallAppText { get; set; }

    [NotNull]
    private string? InstallText { get; set; }

    [NotNull]
    private string? CancelText { get; set; }

    [NotNull]
    private string? ThemeTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        FlowText ??= Localizer[nameof(FlowText)];
        InstallAppText ??= Localizer[nameof(InstallAppText)];
        InstallText ??= Localizer[nameof(InstallText)];
        CancelText ??= Localizer[nameof(CancelText)];
        ThemeTooltip ??= Localizer[nameof(ThemeTooltip)];

        CommitDispatchService.Subscribe(NotifyCommit);
        RebootDispatchService.Subscribe(NotifyReboot);
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
