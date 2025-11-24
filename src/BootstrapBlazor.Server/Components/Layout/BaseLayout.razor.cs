// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// 母版页基类
/// </summary>
public partial class BaseLayout : IAsyncDisposable
{
    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

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

    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

    [NotNull]
    private string? FlowText { get; set; }

    [NotNull]
    private string? InstallAppText { get; set; }

    [NotNull]
    private string? InstallText { get; set; }

    [NotNull]
    private string? CancelText { get; set; }

    private bool _init = false;
    private JSModule? _module;
    private DotNetObjectReference<BaseLayout>? _interop;

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

        CommitDispatchService.Subscribe(NotifyCommit);
        RebootDispatchService.Subscribe(NotifyReboot);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _module = await JSRuntime.LoadModule($"{WebsiteOption.Value.JSModuleRootPath}Layout/BaseLayout.razor.js");
            _interop = DotNetObjectReference.Create(this);
            await _module.InvokeVoidAsync("doTask", _interop);
            _init = true;
            StateHasChanged();
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
    /// 显示投票弹窗
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task ShowVoteToast()
    {
        // 英文环境不投票
        if (CultureInfo.CurrentUICulture.Name == "en-US")
        {
            return;
        }

        _option = new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "邀请您支持 BB 参与 Gitee 项目评选活动",
            IsAutoHide = false,
            ChildContent = RenderVote,
            PreventDuplicates = true,
            ClassString = "bb-vote-toast"
        };
        await Toast.Show(_option);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="disposing"></param>
    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            CommitDispatchService.UnSubscribe(NotifyCommit);
            RebootDispatchService.UnSubscribe(NotifyReboot);

            if (_module != null)
            {
                await _module.InvokeVoidAsync("dispose");
                await _module.DisposeAsync();
            }
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
