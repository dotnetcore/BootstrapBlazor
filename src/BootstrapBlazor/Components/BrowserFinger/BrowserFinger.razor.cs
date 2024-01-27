// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器指纹组件
/// </summary>
[BootstrapModuleAutoLoader("BrowserFinger/BrowserFinger.razor.js", AutoInvokeInit = false, AutoInvokeDispose = false)]
public partial class BrowserFinger : IDisposable
{
    [Inject]
    [NotNull]
    private IBrowserFingerService? BrowserFingerService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        BrowserFingerService.Subscribe(this, Callback);
    }

    private async Task<string?> Callback() => await InvokeAsync<string?>("getFingerCode");

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            BrowserFingerService.Unsubscribe(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
