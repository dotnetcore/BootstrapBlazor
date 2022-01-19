// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax组件
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
    }

    private async Task<string?> GetMessage(AjaxOption option)
    {
        var obj = await JSRuntime.InvokeAsync<string?>(identifier: "$.bb_ajax", option.Url, option.Method, option.Data);
        return obj;
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            AjaxService.UnRegister(this);
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
