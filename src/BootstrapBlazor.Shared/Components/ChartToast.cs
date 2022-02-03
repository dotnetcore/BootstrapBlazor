// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public partial class ChartToast : ComponentBase, IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    [Inject]
    private ToastService? ToastService { get; set; }

    [Inject]
    private IJSRuntime? JSRuntime { get; set; }

    private JSInterop<ChartToast>? Interope { get; set; }

    /// <summary>
    /// BuildRenderTree 方法
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<Toast>(0);
        builder.CloseComponent();
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && JSRuntime != null)
        {
            if (Interope == null) Interope = new JSInterop<ChartToast>(JSRuntime);
            await Interope.InvokeVoidAsync(this, "", "_initChart", nameof(ShowToast));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JSInvokable]
    public void ShowToast()
    {
        ToastService?.Show(new ToastOption() { Title = "友情提示", Content = "屏幕宽度过小，如果是手机请横屏观看" });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing)
    {
        if (disposing) Interope?.Dispose();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
