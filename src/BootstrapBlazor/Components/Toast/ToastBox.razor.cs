// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class ToastBox : IDisposable
{
    private MarkupString MarkupContent => string.IsNullOrEmpty(Options.Content) ? new MarkupString() : new MarkupString(Options.Content);
    /// <summary>
    /// ToastBox HTML 实例引用
    /// </summary>
    protected ElementReference ToastBoxElement { get; set; }

    /// <summary>
    /// 获得/设置 弹出框类型
    /// </summary>
    protected string? AutoHide => !Options.IsAutoHide ? "false" : null;

    /// <summary>
    /// 获得/设置 弹出框类型
    /// </summary>
    protected string? ClassName => CssBuilder.Default("toast fade")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 进度条样式
    /// </summary>
    protected string? ProgressClass => CssBuilder.Default("toast-progress")
        .AddClass($"bg-{Options.Category.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// 获得/设置 图标样式
    /// </summary>
    protected string? IconString => CssBuilder.Default("fa")
        .AddClass("fa-check-circle text-success", Options.Category == ToastCategory.Success)
        .AddClass("fa-exclamation-circle text-info", Options.Category == ToastCategory.Information)
        .AddClass("fa-times-circle text-danger", Options.Category == ToastCategory.Error)
        .AddClass("fa-exclamation-triangle text-warning", Options.Category == ToastCategory.Warning)
        .Build();

    /// <summary>
    /// 获得/设置 弹出框自动关闭时长
    /// </summary>
    protected string? DelayString => Options.IsAutoHide ? Convert.ToString(Options.Delay + 200) : null;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public ToastOption? Options { get; set; }

    /// <summary>
    /// 获得/设置 Toast 实例
    /// </summary>
    /// <value></value>
    [CascadingParameter]
    public Toast? Toast { get; set; }

    private JSInterop<Toast>? Interop { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Options.ToastBox = this;
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        // 执行客户端动画
        if (firstRender)
        {
            if (Toast != null)
            {
                Interop = new JSInterop<Toast>(JSRuntime);
                await Interop.InvokeVoidAsync(Toast, ToastBoxElement, "bb_toast", nameof(Toast.Clear));
            }
        }
    }

    internal ValueTask Close() => JSRuntime.InvokeVoidAsync(ToastBoxElement, "bb_toast_close");

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && Interop != null)
        {
            Interop.Dispose();
            Interop = null;
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
