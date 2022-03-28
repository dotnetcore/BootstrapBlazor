// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class IconDialog : IAsyncDisposable
{
    [Inject]
    [NotNull]
    private ClipboardService? ClipboardService { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? IconName { get; set; }

    [NotNull]
    private Button? ButtonIcon { get; set; }

    [NotNull]
    private Button? ButtonFullIcon { get; set; }

    private string IconFullName => $"<i class=\"{IconName}\" aria-hidden=\"true\"></i>";

    private CancellationTokenSource CopyIconTokenSource { get; } = new();

    private CancellationTokenSource CopyFullIconTokenSource { get; } = new();

    private Task OnClickCopyIcon() => ClipboardService.Copy(IconName, async () =>
    {
        await ButtonIcon.ShowTooltip("拷贝成功");
        try
        {
            await Task.Delay(1000, CopyIconTokenSource.Token);
            await ButtonIcon.RemoveTooltip();
        }
        catch (TaskCanceledException)
        {

        }
    });

    private Task OnClickCopyFullIcon() => ClipboardService.Copy(IconFullName, async () =>
    {
        await ButtonFullIcon.ShowTooltip("拷贝成功");
        try
        {
            await Task.Delay(1000, CopyFullIconTokenSource.Token);
            await ButtonFullIcon.RemoveTooltip();
        }
        catch (TaskCanceledException)
        {

        }
    });

    private async Task OnClickClose()
    {
        if (OnClose != null)
        {
            await OnClose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        if (CopyIconTokenSource.IsCancellationRequested)
        {
            await ButtonIcon.RemoveTooltip();
        }
        CopyIconTokenSource.Dispose();

        if (CopyFullIconTokenSource.IsCancellationRequested)
        {
            await ButtonFullIcon.RemoveTooltip();
        }
        CopyFullIconTokenSource.Dispose();

        GC.SuppressFinalize(this);
    }
}
