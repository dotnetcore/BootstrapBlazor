// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System;

namespace BootstrapBlazor.Components;

/// <summary>
/// 下载组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "download", AutoInvokeInit = false, AutoInvokeDispose = false)]
public class Download : BootstrapModuleComponentBase
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

        DownloadService.RegisterStream(this, DownloadFromStream);
        DownloadService.RegisterUrl(this, DownloadFromUrl);
    }

    /// <summary>
    /// 调用 download 方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    protected virtual async Task DownloadFromStream(DownloadOption option)
    {
        if (option.FileStream == null)
        {
            throw new InvalidOperationException($"the {nameof(option.FileStream)} is null");
        }

        if (string.IsNullOrEmpty(option.FileName))
        {
            throw new InvalidOperationException($"the  {nameof(option.FileName)} is null or empty");
        }

        using var streamRef = new DotNetStreamReference(option.FileStream);
        await InvokeVoidAsync("downloadFileFromStream", option.FileName, streamRef);
    }

    /// <summary>
    /// 调用 CreateUrl 方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    protected virtual async Task DownloadFromUrl(DownloadOption option)
    {
        if (string.IsNullOrEmpty(option.Url))
        {
            throw new InvalidOperationException($"{nameof(option.Url)} not set");
        }

        if (string.IsNullOrEmpty(option.FileName))
        {
            throw new InvalidOperationException($"the  {nameof(option.FileName)} is null or empty");
        }

        await InvokeVoidAsync("downloadFileFromUrl", option.FileName, option.Url);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            DownloadService.UnRegisterStream(this);
            DownloadService.UnRegisterUrl(this);
        }

        await base.DisposeAsync(disposing);
    }
}
