// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认定位实现类
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
public class DefaultIPLocator : IIPLocator
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public virtual Task<string?> Locate(IPLocatorOption option) => Task.FromResult<string?>(null);

    /// <summary>
    /// 获得/设置 IP定位器请求地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 泛型定位方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    protected virtual async Task<string?> Locate<T>(IPLocatorOption option)
    {
        string? ret = null;
        if (!string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(option.IP) && option.HttpClient != null)
        {
            var url = string.Format(Url, option.IP);
            try
            {
                using var token = new CancellationTokenSource(option.RequestTimeout);
                var result = await option.HttpClient.GetFromJsonAsync<T>(url, token.Token);
                if (result != null)
                {
                    ret = result.ToString();
                }
            }
            catch (Exception ex)
            {
                option.Logger?.LogError(ex, "Url: {url}", url);
            }
        }
        return ret;
    }
}
