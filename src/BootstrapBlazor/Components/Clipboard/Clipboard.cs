// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Clipboard 组件部分类
/// </summary>
[JSModuleAutoLoader("base/utility")]
public class Clipboard : BootstrapModule2ComponentBase
{
    /// <summary>
    /// DialogServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private ClipboardService? ClipboardService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 ClipboardService 弹窗事件
        ClipboardService.Register(this, Copy);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task ModuleInvokeVoidAsync(bool firstRender) => Task.CompletedTask;

    private async Task Copy(ClipboardOption option)
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync("copy", option.Text);
        }
        if (option.Callback != null)
        {
            await option.Callback();
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            ClipboardService.UnRegister(this);
        }
        await base.DisposeAsync(disposing);
    }
}
