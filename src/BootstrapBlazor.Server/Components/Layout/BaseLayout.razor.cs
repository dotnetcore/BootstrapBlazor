// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// 母版页基类
/// </summary>
public partial class BaseLayout
{
    [Inject]
    [NotNull]
    private IStringLocalizer<BaseLayout>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? WebsiteOption { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<GiteePostBody>? DispatchService { get; set; }

    [NotNull]
    private string? FlowText { get; set; }

    [NotNull]
    private string? InstallAppText { get; set; }

    [NotNull]
    private string? InstallText { get; set; }

    [NotNull]
    private string? CancelText { get; set; }

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

        FlowText ??= Localizer[nameof(FlowText)];
        InstallAppText ??= Localizer[nameof(InstallAppText)];
        InstallText ??= Localizer[nameof(InstallText)];
        CancelText ??= Localizer[nameof(CancelText)];
        Title ??= Localizer[nameof(Title)];
        ChatTooltip ??= Localizer[nameof(ChatTooltip)];

        DispatchService.Subscribe(Notify);
    }

    private async Task Notify(DispatchEntry<GiteePostBody> payload)
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            DispatchService.UnSubscribe(Notify);
        }
        await base.DisposeAsync(disposing);
    }
}
