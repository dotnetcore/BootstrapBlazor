// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 IP 地理位置定位器
/// </summary>
public abstract class DefaultIpLocatorProvider : IIpLocatorProvider
{
    /// <summary>
    /// 获得 Ip 定位结果缓存
    /// </summary>
    protected MemoryCache IpCache { get; } = new(new MemoryCacheOptions());

    /// <summary>
    /// 获得 IpLocator 配置信息
    /// </summary>
    protected IpLocatorOptions Options { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    protected DefaultIpLocatorProvider(IOptions<BootstrapBlazorOptions> options)
    {
        Options = options.Value.IpLocatorOptions;
        Key = GetType().Name;
    }

    /// <summary>
    /// 获得 本机地址列表
    /// </summary>
    private readonly List<string> _localhostList = [.. new[] { "::1", "127.0.0.1" }];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<string?> Locate(string? ip)
    {
        string? ret = null;

        // 解析本机地址
        if (string.IsNullOrEmpty(ip) || _localhostList.Any(p => p == ip))
        {
            ret = "localhost";
        }
        else if (Options.EnableCache)
        {
            if (IpCache.TryGetValue(ip, out var v) && v is string city && !string.IsNullOrEmpty(city))
            {
                ret = city;
            }
            else
            {
                ret = await LocateByIp(ip);
                if (!string.IsNullOrEmpty(ret))
                {
                    using var entry = IpCache.CreateEntry(ip);
                    entry.Value = ret;
                    entry.SetSlidingExpiration(Options.SlidingExpiration);
                }
            }
        }
        else
        {
            ret = await LocateByIp(ip);
        }
        return ret;
    }

    /// <summary>
    /// 内部定位方法
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    protected abstract Task<string?> LocateByIp(string ip);
}
