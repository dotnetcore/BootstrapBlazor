// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Toast 弹出窗组件
/// </summary>
public partial class ToastContainer : IDisposable
{
    private string? ClassString => CssBuilder.Default("toast-container")
        .AddClass("top-0 start-0", Placement == Placement.TopStart)
        .AddClass("top-0 start-50 translate-middle-x", Placement == Placement.TopCenter)
        .AddClass("top-0 end-0", Placement == Placement.TopEnd)
        .AddClass("top-50 start-0 translate-middle-y", Placement == Placement.MiddleStart)
        .AddClass("top-50 start-50 translate-middle", Placement == Placement.MiddleCenter)
        .AddClass("top-50 end-0 translate-middle-y", Placement == Placement.MiddleEnd)
        .AddClass("bottom-0 start-0", Placement == Placement.BottomStart)
        .AddClass("bottom-0 start-50 translate-middle-x", Placement == Placement.BottomCenter)
        .AddClass("bottom-0 end-0", Placement == Placement.BottomEnd)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetToastBoxClassString(string? classString) => CssBuilder.Default(classString)
        .AddClass("left", Placement == Placement.TopStart)
        .AddClass("left", Placement == Placement.MiddleStart)
        .AddClass("left", Placement == Placement.BottomStart)
        .AddClass("left", Placement == Placement.TopCenter)
        .AddClass("left", Placement == Placement.MiddleCenter)
        .AddClass("left", Placement == Placement.BottomCenter)
        .Build();

    private static string? GetToastBoxStyleString(string? styleString) => CssBuilder.Default(styleString)
        .Build();

    /// <summary>
    /// 获得 弹出窗集合
    /// </summary>
    private List<ToastOption> Toasts { get; } = [];

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public Placement Placement { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Placement = Options.CurrentValue.ToastPlacement ?? Placement.BottomEnd;

        // 注册 Toast 弹窗事件
        ToastService.Register(this, Show);
    }

    private async Task Show(ToastOption option)
    {
        if (option.PreventDuplicates)
        {
            var lastOption = Toasts.LastOrDefault();
            if (lastOption != null && option.Title == lastOption.Title && option.Content == lastOption.Content)
            {
                return;
            }
        }

        // support update content
        if (!Toasts.Contains(option))
        {
            Toasts.Add(option);
        }
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 关闭弹窗
    /// </summary>
    /// <param name="option"></param>
    public async Task Close(ToastOption option)
    {
        if (option.OnCloseAsync != null)
        {
            await option.OnCloseAsync();
        }
        Toasts.Remove(option);
        StateHasChanged();
    }

    /// <summary>
    /// 设置 Toast 容器位置方法
    /// </summary>
    /// <param name="placement"></param>
    public void SetPlacement(Placement placement)
    {
        Placement = placement;
        StateHasChanged();
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ToastService.UnRegister(this);
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
