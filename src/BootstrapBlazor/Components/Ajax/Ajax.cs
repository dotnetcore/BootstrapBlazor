// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax 组件
/// </summary>
public class Ajax : BootstrapComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private AjaxService? AjaxService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        AjaxService.Register(this, GetMessage);
        AjaxService.RegisterGoto(this, Goto);
    }

    private async Task<string?> GetMessage(AjaxOption option)
    {
        var obj = await JSRuntime.InvokeAsync<string?>(null, "bb_ajax", option.Url, option.Method, option.Data);
        return obj;
    }

    private async Task Goto(string url)
    {
        await JSRuntime.InvokeVoidAsync(null, "bb_ajax_goto", url);
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            AjaxService.UnRegister(this);
            AjaxService.UnRegisterGoto(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
