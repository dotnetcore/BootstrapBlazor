// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Extensions;
using System.Reflection;

namespace BootstrapBlazor.Shared;

/// <summary>
/// App 组件
/// </summary>
public partial class App
{
    [Inject]
    [NotNull]
    private IStringLocalizer<App>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<GiteePostBody>? DispatchService { get; set; }

    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    [NotNull]
    private IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AdditionalAssemblies = new Assembly[]
        {
            typeof(BootstrapBlazor.Shared.Pages.Index).Assembly
        };
        DispatchService.Subscribe(Notify);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Localizer["ErrorMessage"].Value, Localizer["Reload"].Value);

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
