// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ConnectionHubOptions 配置类</para>
/// <para lang="en">ConnectionHubOptions configuration class</para>
/// </summary>
public class ConnectionHubOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否开启 ConnectionHub 功能 默认 false 未开启</para>
    /// <para lang="en">Gets or sets whether to enable ConnectionHub feature default false</para>
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启 IP 定位功能 默认 false 未开启</para>
    /// <para lang="en">Gets or sets whether to enable IP locator feature default false</para>
    /// </summary>
    public bool EnableIpLocator { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过期扫描周期 默认 1 分钟</para>
    /// <para lang="en">Gets or sets the expiration scan frequency. The default is 1 minute.</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">
    ///     <para>定期清理最后一次心跳超过 <see cref="TimeoutInterval"/> 的连接。在线数 <see cref="IConnectionService.Count"/> 按超时间隔计算，不受扫描周期影响。</para>
    ///     <para>启用时须在 1 至 4294967294 毫秒之间。</para>
    /// </para>
    /// <para lang="en">
    ///     <para>Periodically removes connections whose last heartbeat exceeds <see cref="TimeoutInterval"/>. The online count <see cref="IConnectionService.Count"/> is based on the timeout regardless of the scan frequency.</para>
    ///     <para>When enabled, this must be between 1 and 4294967294 milliseconds.</para>
    /// </para>
    /// </remarks>
    public TimeSpan ExpirationScanFrequency { get; set; } = TimeSpan.FromMinutes(1);

    /// <summary>
    /// <para lang="zh">获得/设置 超时间隔，默认 2 分钟</para>
    /// <para lang="en">Gets or sets the timeout interval. The default is 2 minutes.</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">启用时必须大于 B + max(4P, ceil(B/2) + P) + 1 毫秒；B 为向上取整到毫秒的心跳间隔，P = max(4, ceil(心跳间隔/8)) 毫秒。请为网络延迟及浏览器定时器限速留出额外余量；无法保证实时切换。</para>
    /// <para lang="en">When enabled, this must be greater than B + max(4P, ceil(B/2) + P) + 1 ms, where B is the heartbeat interval rounded up to milliseconds and P = max(4, ceil(heartbeat interval/8)) ms. Allow extra time for network delays and browser timer throttling; failover is not guaranteed in real time.</para>
    /// </remarks>
    public TimeSpan TimeoutInterval { get; set; } = TimeSpan.FromMinutes(2);

    /// <summary>
    /// <para lang="zh">获得/设置 ConnectionHub 组件心跳间隔，默认 30 秒</para>
    /// <para lang="en">Gets or sets the ConnectionHub component heartbeat interval. The default is 30 seconds.</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">启用时须在 1 至 2147483647 毫秒之间。请在服务初始化前完成所有间隔配置，运行时不要修改。</para>
    /// <para lang="en">When enabled, this must be between 1 and 2147483647 milliseconds. Configure all intervals before service initialization and do not change them at runtime.</para>
    /// </remarks>
    public TimeSpan BeatInterval { get; set; } = TimeSpan.FromSeconds(30);

    internal void Validate()
    {
        if (BeatInterval < TimeSpan.FromMilliseconds(1) || BeatInterval > TimeSpan.FromMilliseconds(int.MaxValue))
        {
            throw new InvalidOperationException("ConnectionHub BeatInterval must be between 1 and 2147483647 milliseconds.");
        }

        if (ExpirationScanFrequency < TimeSpan.FromMilliseconds(1) || ExpirationScanFrequency > TimeSpan.FromMilliseconds(uint.MaxValue - 1))
        {
            throw new InvalidOperationException("ConnectionHub ExpirationScanFrequency must be between 1 and 4294967294 milliseconds.");
        }

        // Cover lease expiry after a missed beat and a throttled takeover retry, including timer rounding.
        var beatMilliseconds = (BeatInterval.Ticks + TimeSpan.TicksPerMillisecond - 1) / TimeSpan.TicksPerMillisecond;
        var pollMilliseconds = Math.Max(4, (BeatInterval.Ticks + 8 * TimeSpan.TicksPerMillisecond - 1) / (8 * TimeSpan.TicksPerMillisecond));
        var minimumTimeoutTicks = (beatMilliseconds + Math.Max(4 * pollMilliseconds, (beatMilliseconds + 1) / 2 + pollMilliseconds) + 1) * TimeSpan.TicksPerMillisecond;
        if (TimeoutInterval.Ticks <= minimumTimeoutTicks)
        {
            throw new InvalidOperationException($"ConnectionHub TimeoutInterval must be greater than {TimeSpan.FromTicks(minimumTimeoutTicks)} for the configured BeatInterval.");
        }
    }
}
