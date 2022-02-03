// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 提供 Tooltip 功能的组件
/// </summary>
public abstract class TooltipComponentBase : IdComponentBase, ITooltipHost, IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 ITooltip 实例
    /// </summary>
    public ITooltip? Tooltip { get; set; }

    private bool IsInited { get; set; }

    /// <summary>
    /// OnAfterRenderAsync
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && Tooltip != null)
        {
            if (Tooltip.PopoverType == PopoverType.Tooltip)
            {
                await ShowTooltip();
            }
            else
            {
                await ShowPopover();
            }
            IsInited = true;
        }
    }

    /// <summary>
    /// 调用 $.bb_tooltip 脚本方法
    /// </summary>
    /// <returns></returns>
    protected virtual async ValueTask ShowTooltip()
    {
        var id = RetrieveId();
        var method = RetrieveMethod();
        if (!string.IsNullOrEmpty(id))
        {
            await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", id, method, RetrieveTitle(), RetrievePlacement(), RetrieveIsHtml(), RetrieveTrigger());
        }
    }

    /// <summary>
    /// 调用 $.bb_popover 脚本方法
    /// </summary>
    /// <returns></returns>
    protected virtual async ValueTask ShowPopover()
    {
        var id = RetrieveId();
        var method = RetrieveMethod();
        if (!string.IsNullOrEmpty(id))
        {
            await JSRuntime.InvokeVoidAsync(null, "bb_popover", id, method, RetrieveTitle(), RetrieveContent(), RetrievePlacement(), RetrieveIsHtml(), RetrieveTrigger());
        }
    }

    /// <summary>
    /// 获得 弹窗客户端 ID
    /// </summary>
    protected virtual string? RetrieveId() => Id;

    /// <summary>
    /// 获得 弹窗脚本执行方法
    /// </summary>
    /// <returns></returns>
    protected virtual string RetrieveMethod() => "";

    /// <summary>
    /// 获得 弹窗标题方法
    /// </summary>
    /// <returns></returns>
    protected virtual string RetrieveTitle() => Tooltip?.Title ?? "";

    /// <summary>
    /// 获得 弹窗内容方法
    /// </summary>
    /// <returns></returns>
    protected virtual string RetrieveContent() => Tooltip?.Content ?? "";

    /// <summary>
    /// 获得 弹窗位置
    /// </summary>
    /// <returns></returns>
    protected virtual string RetrievePlacement() => Tooltip?.Placement.ToDescriptionString() ?? "top";

    /// <summary>
    /// 获得 弹窗内容是否为 Html 方法
    /// </summary>
    /// <returns></returns>
    protected virtual bool RetrieveIsHtml() => Tooltip?.IsHtml ?? false;

    /// <summary>
    /// 获得 弹窗激活方法
    /// </summary>
    /// <returns></returns>
    protected virtual string RetrieveTrigger() => Tooltip?.Trigger ?? "hover focus";

    /// <summary>
    /// DisposeAsyncCore 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing && Tooltip != null && IsInited)
        {
            var id = RetrieveId();
            if (!string.IsNullOrEmpty(id))
            {
                if (Tooltip.PopoverType == PopoverType.Tooltip)
                {
                    await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", id, "dispose");
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync(null, "bb_popover", id, "dispose");
                }
            }
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore(true);
        GC.SuppressFinalize(this);
    }
}
