// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DateTime 相关全局配置类</para>
/// </summary>
public class DateTimeSettings
{
    /// <summary>
    /// <para lang="zh">自定义解析日期时间的方法</para>
    /// </summary>
    public Func<string, DateTime>? ParseDateTimeCallback { get; set; }

    /// <summary>
    /// <para lang="zh">自定义解析日期时间偏移的方法</para>
    /// </summary>
    public Func<string, DateTimeOffset>? ParseDateTimeOffsetCallback { get; set; }
}
