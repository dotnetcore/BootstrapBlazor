// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
///
/// </summary>
public class DefaultIPLocator : IIPLocator
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public virtual Task<string?> Locate(IPLocatorOption option) => Task.FromResult<string?>(null);

    /// <summary>
    /// 获得/设置 IP定位器请求地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    protected async Task<string?> Locate<T>(IPLocatorOption option) where T : class
    {
        string? ret = null;
        try
        {
            if (!string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(option.IP) && option.HttpClient != null)
            {
                var url = string.Format(Url, option.IP);
                using var token = new CancellationTokenSource(option.RequestTimeout);
                var result = await option.HttpClient.GetFromJsonAsync<T>(url, token.Token);
                ret = result?.ToString();
            }
        }
        catch (Exception ex)
        {
            option.Logger?.LogError(ex, Url, option.IP);
        }
        return ret;
    }
}
