// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreen 组件部分类
/// </summary>
public partial class FullScreen : BootstrapComponentBase, IDisposable
{
    /// <summary>
    /// DialogServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private FullScreenService? FullScreenService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 FullScreen 弹窗事件
        FullScreenService.Register(this, Show);
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Option != null)
        {
            await JSRuntime.InvokeVoidAsync(Option.Element.Context != null ? Option.Element : "", "bb_toggleFullscreen", Option.Id ?? "");
            Option = null;
        }
    }

    private FullScreenOption? Option { get; set; }

    private Task Show(FullScreenOption option)
    {
        Option = option;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            FullScreenService.UnRegister(this);
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
