// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 下载组件
/// </summary>
public class Download : BootstrapComponentBase, IDisposable
{
    [Inject]
    [NotNull]
    private DownloadService? DownloadService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DownloadService.Register(this, DownloadFile);
        DownloadService.RegisterUrl(this, CreateUrl);
    }

    private async Task DownloadFile(DownloadOption option)
    {
        if (JSRuntime is IJSUnmarshalledRuntime webAssemblyJsRuntime)
        {
            webAssemblyJsRuntime.InvokeUnmarshalled<string?, string, byte[], bool>("$.bb_download_wasm", option.FileName,
                option.Mime, option.FileContent);
        }
        else
        {
            await JSRuntime.InvokeVoidAsync(identifier: "$.bb_download", option.FileName, option.Mime, option.FileContent);
        }
    }

    private async Task<string> CreateUrl(DownloadOption option)
    {
        if (JSRuntime is IJSUnmarshalledRuntime webAssemblyJsRuntime)
        {
            return webAssemblyJsRuntime.InvokeUnmarshalled<string?, string, byte[], string>("$.bb_create_url_wasm", option.FileName,
                option.Mime, option.FileContent);
        }
        else
        {
            return await JSRuntime.InvokeAsync<string>(identifier: "$.bb_create_url", option.FileName, option.Mime, option.FileContent);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DownloadService.UnRegister(this);
            DownloadService.UnRegisterUrl(this);
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
