// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">默认 IP 地理位置定位器</para>
/// <para lang="en">Default IP Geolocation Locator</para>
/// </summary>
public abstract class DefaultIpLocatorProvider : IIpLocatorProvider
{
    /// <summary>
    /// <para lang="zh">获得 Ip 定位结果缓存</para>
    /// <para lang="en">Get IP Location Cache</para>
    /// </summary>
    protected MemoryCache IpCache { get; } = new(new MemoryCacheOptions());

    /// <summary>
    /// <para lang="zh">获得 IpLocator 配置信息</para>
    /// <para lang="en">Get IpLocator Configuration Options</para>
    /// </summary>
    protected IpLocatorOptions Options { get; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    protected DefaultIpLocatorProvider(IOptions<BootstrapBlazorOptions> options)
    {
        Options = options.Value.IpLocatorOptions;
        Key = GetType().Name;
    }

    /// <summary>
    /// <para lang="zh">获得 本机地址列表</para>
    /// <para lang="en">Get Localhost List</para>
    /// </summary>
    private readonly List<string> _localhostList = [.. new[] { "::1", "127.0.0.1" }];

    /// <summary>
    /// <inheritdoc cref="IIpLocatorProvider.Key"/>
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
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
    /// <para lang="zh">内部定位方法</para>
    /// <para lang="en">Internal Locate Method</para>
    /// </summary>
    /// <param name="ip"></param>
    protected abstract Task<string?> LocateByIp(string ip);
}
