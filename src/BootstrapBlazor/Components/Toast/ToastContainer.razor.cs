// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toast 弹出窗容器组件</para>
/// <para lang="en">Toast Popup Window Container Component</para>
/// </summary>
public partial class ToastContainer
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

    private List<ToastOption> Toasts { get; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗位置</para>
    /// <para lang="en">Gets or sets the popup window placement</para>
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
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Placement = Options.CurrentValue.ToastPlacement ?? Placement.BottomEnd;

        // 注册 Toast 弹窗事件
        ToastService.Register(this, Show);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        await InvokeVoidAsync("toggle", Id, Toasts.Count > 0);
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
    /// <para lang="zh">关闭弹窗方法</para>
    /// <para lang="en">Closes the popup window</para>
    /// </summary>
    /// <param name="option"></param>
    public async Task Close(ToastOption option)
    {
        if (!Toasts.Remove(option))
        {
            return;
        }

        StateHasChanged();
        if (option.OnCloseAsync != null)
        {
            await option.OnCloseAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">设置 Toast 容器位置方法</para>
    /// <para lang="en">Sets the Toast container position</para>
    /// </summary>
    /// <param name="placement"></param>
    public void SetPlacement(Placement placement)
    {
        Placement = placement;
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            ToastService.UnRegister(this);
        }

        await base.DisposeAsync(disposing);
    }
}
