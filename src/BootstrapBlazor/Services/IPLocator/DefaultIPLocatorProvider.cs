// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 IP 地理位置定位器
/// </summary>
public abstract class DefaultIPLocatorProvider : IIPLocatorProvider
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public DefaultIPLocatorProvider()
    {
        Key = GetType().Name;
    }

    /// <summary>
    /// 获得 本机地址列表
    /// </summary>
    private readonly List<string> _localhosts = new(new string[] { "::1", "127.0.0.1" });

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual object? Key { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<string?> Locate(string ip)
    {
        string? ret = null;

        // 解析本机地址
        if (string.IsNullOrEmpty(ip) || _localhosts.Any(p => p == ip))
        {
            ret = "本地连接";
        }
        else
        {
            await LocateByIp(ip);
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
