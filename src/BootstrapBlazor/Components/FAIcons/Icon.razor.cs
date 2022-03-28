// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Icon : IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 图标名称
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 点击时是否显示高级拷贝弹窗 默认 false 直接拷贝到粘贴板
    /// </summary>
    [Parameter]
    public bool ShowCopyDialog { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    [Inject]
    [NotNull]
    private ClipboardService? ClipboardService { get; set; }

    private CancellationTokenSource? CopyIconTokenSource { get; set; }

    private async Task OnClickIcon()
    {
        if (ShowCopyDialog)
        {
            await DialogService.ShowCloseDialog<IconDialog>("请选择图标", parameters =>
            {
                parameters.Add(nameof(IconDialog.IconName), Name);
            });
        }
        else
        {
            await ClipboardService.Copy(Name, async () =>
            {
                await ShowTooltip("拷贝成功");
                try
                {
                    CopyIconTokenSource ??= new();
                    await Task.Delay(1000, CopyIconTokenSource.Token);
                    await RemoveTooltip();
                }
                catch (TaskCanceledException)
                {

                }
            });
        }
    }

    /// <summary>
    /// 显示 Tooltip 方法
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual async ValueTask ShowTooltip(string message)
    {
        if (!string.IsNullOrEmpty(Id))
        {
            await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", Id, "show", message, "top", false, "hover");
        }
    }

    /// <summary>
    /// 销毁 Tooltip 方法
    /// </summary>
    /// <returns></returns>
    public virtual async ValueTask RemoveTooltip()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", Id, "dispose");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            if (CopyIconTokenSource != null)
            {
                if (CopyIconTokenSource.IsCancellationRequested)
                {
                    await RemoveTooltip();
                }
                CopyIconTokenSource.Dispose();
                CopyIconTokenSource = null;
            }
        }

        await base.DisposeAsyncCore(disposing);
    }
}
