// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;
using BootstrapBlazor.Shared.Exntensions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared;

/// <summary>
/// 
/// </summary>
public sealed partial class App : IDisposable
{
    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<App>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<GiteePostBody>? DispatchService { get; set; }

    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DispatchService.Subscribe(Notify);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("$.loading", OperatingSystem.IsBrowser(), Localizer["ErrorMessage"].Value, Localizer["Reload"].Value);
        }
    }

    private async Task Notify(DispatchEntry<GiteePostBody> payload)
    {
        if (payload.CanDispatch())
        {
            var option = new ToastOption()
            {
                Category = ToastCategory.Information,
                Title = "代码提交推送通知",
                Delay = 30 * 1000,
                ForceDelay = true,
#if DEBUG
                IsAutoHide = false,
#endif
                ChildContent = BootstrapDynamicComponent.CreateComponent<CommitItem>(new Dictionary<string, object?>
                {
                    [nameof(CommitItem.Item)] = payload.Entry
                }).Render()
            };
            await Toast.Show(option);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            DispatchService.UnSubscribe(Notify);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
